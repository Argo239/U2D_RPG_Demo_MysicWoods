using UnityEngine;
using UnityEngine.EventSystems;
using static Argo_Utils.Utils;

public class PlayerMove : MonoBehaviour {
    public static PlayerMove Instance { get; private set; }

    private Rigidbody2D rb;
    private Camera mainCamera;
    private GameInput gameInput;
    private PlayerController playerController;

    private float smoothingFactor = 10f;
    private Vector2 moveDirection;
    private Vector2 mouseLookDirection;
    private PlayerController.State currentState;
    private ControllDirection enumDirection;

    private float speed = 5;
    private float runSpeed = 10;
    private bool isRunning;


    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        rb = GetComponent<Rigidbody2D>();
        playerController = PlayerController.Instance;
        gameInput = GameInput.Instance;
        mainCamera = Camera.main;

        if (rb == null) {
            DebugLog(playerController.consoleLogOn, $"Cant found rb component");
        }
    }

    private void Start() {
        gameInput.OnPlayerSprintDogePerformed += GameInput_OnPlayerSprintDogePerformed;
        gameInput.OnPlayerSprintFinished += GameInput_OnPlayerSprintFinished;
    }


    private void GameInput_OnPlayerSprintDogePerformed(object sender, System.EventArgs e) =>
        isRunning = true;

    private void GameInput_OnPlayerSprintFinished(object sender, System.EventArgs e) =>
        isRunning = false;

    private void Update() {
        GetPlayerDirection();
    }

    private void FixedUpdate() {
        Movement();
        PlayerLookDirection();
    }

    /// <summary>
    /// Handles player movement using physics (Rigidbody2D).
    /// Lerp is used for smoothing velocity changes.
    /// </summary>
    private void Movement() {
        // Calculate the target velocity based on move direction and player speed
        Vector2 targetVelocity = moveDirection;
        if (!isRunning) {
            targetVelocity *= speed;
        } else {
            targetVelocity *= runSpeed;
        }
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

    /// <summary>
    /// Retrieves the player's movement input vector and determines the enum direction.
    /// </summary>
    public void GetPlayerDirection() {
        moveDirection = gameInput.GetMovementVectorNormalized();
        enumDirection = GetMoveDirection(moveDirection);
    }

    /// <summary>
    /// Determines the movement direction (as a ControllDirection enum) from the input vector.
    /// If the input vector is zero, returns the last known direction.
    /// </summary>
    /// <param name="direction">Input movement vector.</param>
    /// <returns>A ControllDirection indicating the dominant direction.</returns>
    public ControllDirection GetMoveDirection(Vector2 direction) {
        if (direction == Vector2.zero)
            return enumDirection;

        // Compare absolute x and y values to determine dominant movement direction
        return Mathf.Abs(direction.x) > Mathf.Abs(direction.y)
            ? (direction.x > 0 ? ControllDirection.Right : ControllDirection.Left)
            : (direction.y > 0 ? ControllDirection.Up : ControllDirection.Down);
    }

    /// <summary>
    /// Returns the normalized facing direction based on the current enum direction.
    /// </summary>
    /// <returns>A normalized Vector2 representing the facing direction.</returns>
    public Vector2 GetFacingDir() {
        return enumDirection switch {
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
    public ControllDirection GetEnumDirection() => enumDirection;

    /// <summary>
    /// Returns the current move direction vector.
    /// </summary>
    public Vector2 GetMoveDirection() => moveDirection;

    /// <summary>
    /// Sets the player's current state.
    /// </summary>
    /// <param name="newState">New state to set.</param>
    public void SetCurrentState(PlayerController.State newState) => currentState = newState;
}
