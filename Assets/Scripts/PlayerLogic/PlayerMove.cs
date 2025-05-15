using System;
using System.Collections;
using UnityEngine;
using static Argo_Utils.Utils;

public class PlayerMove : MonoBehaviour {
    public static PlayerMove Instance { get; private set; }

    public event EventHandler OnPlayerStartMoving;
    public event EventHandler OnPlayerStopMoving;
    public event EventHandler OnPlayerStartRunning;
    public event EventHandler OnPlayerStopRunning;
    public event EventHandler OnPlayerDodgePerformed;

    private Rigidbody2D rb;
    private Camera mainCamera;
    private GameInput _gameInput;
    private PlayerController _playerController;

    private float smoothingFactor = 10f;
    private Vector2 inputVector;
    private Vector2 mouseLookDirection;
    private PlayerController.State currentState;
    private ControllDirection cardinalDir;

    private float speed = 5;
    private float runSpeed = 10;
    private bool isRunning;

    private float dodgeCooldown = 2f;
    private float lastDodgeTime = -1f;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _playerController = PlayerController.Instance;
        rb = GetComponent<Rigidbody2D>();
        _gameInput = _playerController.GameInput;
        mainCamera = Camera.main;

        if (rb == null) {
            DebugLog(_playerController.consoleLogOn, $"Cant found rb component");
        }
    }

    private void OnEnable() {
        _gameInput.OnPlayerStartMoving += GameInput_OnPlayerStartMoving;
        _gameInput.OnPlayerStopMoving += GameInput_OnPlayerStopMoving;
        _gameInput.OnPlayerStartRunning += GameInput_OnPlayerStartRunning;
        _gameInput.OnPlayerStopRunning += GameInput_OnPlayerStopRunning;
        _gameInput.OnPlayerDodgePerformed += GameInput_OnPlayerDodgePerformed;
    }

    private void OnDisable() {
        _gameInput.OnPlayerStartMoving -= GameInput_OnPlayerStartMoving;
        _gameInput.OnPlayerStopMoving -= GameInput_OnPlayerStopMoving;
        _gameInput.OnPlayerStartRunning -= GameInput_OnPlayerStartRunning;
        _gameInput.OnPlayerStopRunning -= GameInput_OnPlayerStopRunning;
        _gameInput.OnPlayerDodgePerformed -= GameInput_OnPlayerDodgePerformed;
    }

    private void Update() {
        UpdateMovementInput();
    }

    private void FixedUpdate() {
        Movement();
        PlayerLookDirection();
    }

    private void GameInput_OnPlayerStartMoving(object sender, EventArgs e) => 
        OnPlayerStartMoving.Invoke(sender, e); 

    private void GameInput_OnPlayerStopMoving(object sender, EventArgs e) =>
        OnPlayerStopMoving.Invoke(sender, e);

    private void GameInput_OnPlayerStartRunning(object sender, System.EventArgs e) {
        isRunning = true;
        OnPlayerStartRunning.Invoke(this, e);       
    }

    private void GameInput_OnPlayerStopRunning(object sender, System.EventArgs e) {
        isRunning = false;
        OnPlayerStopMoving?.Invoke(this, e);
    }

    private void GameInput_OnPlayerDodgePerformed(object sender, System.EventArgs e) {
        if (!canDodge) return;

        _gameInput.StunMovement(.5f);

        Vector2 dodgeDir = GetInputVector();
        PlayerDodge(dodgeDir, 1f, .5f, .2f);
        lastDodgeTime = Time.time;

        OnPlayerDodgePerformed.Invoke(this, e);
    }

    /// <summary>
    /// Handles player movement using physics (Rigidbody2D).
    /// Lerp is used for smoothing velocity changes.
    /// </summary>
    private void Movement() {
        // Calculate the target velocity based on move direction and player speed
        Vector2 targetVelocity = inputVector * (isRunning ? runSpeed : speed);
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, smoothingFactor * Time.deltaTime);
    }

    /// <summary>
    /// Calculates and smooths the player's look direction based on the mouse position.
    /// </summary>
    /// <returns>The updated look direction vector.</returns>
    private Vector2 PlayerLookDirection() {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        Vector2 playerPosition = GetPlayerCurrentPosition();
        Vector2 targetDirection = (worldMousePosition - playerPosition).normalized;

        // Smooth the look direction using Lerp
        mouseLookDirection = Vector2.Lerp(mouseLookDirection, targetDirection, smoothingFactor * Time.deltaTime);
        return mouseLookDirection;
    }

    private void PlayerDodge(Vector2 dodgeDirection, float distance, float duration, float jumpHeight) {
        StartCoroutine(DodgeCoroutine(dodgeDirection, distance, duration, jumpHeight));
    }

    private IEnumerator DodgeCoroutine(Vector2 dodgeDirection, float distance, float duration, float jumpHeight) {
        Vector3 startPos = PlayerController.Instance.GetPlayerCurrentPosition();
        float timer = 0f;
        while (timer < duration) {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);
            Vector2 horizontalOffset = dodgeDirection * distance * t;
            float verticalOffset = 4f * jumpHeight * t * (1f - t);
            rb.MovePosition(startPos + (Vector3)horizontalOffset + new Vector3(0, verticalOffset, 0));

            yield return null;
        }
        rb.MovePosition(startPos + (Vector3)(dodgeDirection * distance));
    }

    /// <summary>
    /// Retrieves the player's movement input vector and determines the enum direction.
    /// </summary>
    public void UpdateMovementInput() {
        inputVector = _gameInput.GetMovementVectorNormalized();
        cardinalDir = CalculateCardinalDir(inputVector);

        Debug.Log($"{inputVector}, {cardinalDir}");
    }

    /// <summary>
    /// Determines the movement direction (as a ControllDirection enum) from the input vector.
    /// If the input vector is zero, returns the last known direction.
    /// </summary>
    /// <param name="direction">Input movement vector.</param>
    /// <returns>A ControllDirection indicating the dominant direction.</returns>
    public ControllDirection CalculateCardinalDir(Vector2 direction) {
        if (direction == Vector2.zero) return cardinalDir;
        // Compare absolute x and y values to determine dominant movement direction
        return Mathf.Abs(direction.x) > Mathf.Abs(direction.y)
            ? (direction.x > 0 ? ControllDirection.Right : ControllDirection.Left)
            : (direction.y > 0 ? ControllDirection.Up : ControllDirection.Down);
    }

    /// <summary>
    /// Returns the normalized facing direction based on the current enum direction.
    /// </summary>
    /// <returns>A normalized Vector2 representing the facing direction.</returns>
    public Vector2 GetFacingVector() {
        return cardinalDir switch {
            ControllDirection.Down => Vector2.down,
            ControllDirection.Up => Vector2.up,
            ControllDirection.Left => Vector2.left,
            ControllDirection.Right => Vector2.right,
            _ => Vector2.zero
        };
    }

    /// <summary>
    /// Returns the player's current world position.
    /// </summary>
    /// <returns>Player position as a Vector2.</returns>
    public Vector2 GetPlayerCurrentPosition() => transform.position;

    /// <summary>
    /// Gets the player's current state.
    /// </summary>
    public PlayerController.State GetCurrentState() => currentState;

    /// <summary>
    /// Returns the current ControllDirection enum.
    /// </summary>
    public ControllDirection GetCardinalDir() => cardinalDir;

    /// <summary>
    /// Returns the current move direction vector.
    /// </summary>
    public Vector2 GetInputVector() => inputVector;

    /// <summary>
    /// Sets the player's current state.
    /// </summary>
    /// <param name="newState">New state to set.</param>
    public void SetCurrentState(PlayerController.State newState) => currentState = newState;

    public bool canDodge => Time.time >= lastDodgeTime + dodgeCooldown;
}
