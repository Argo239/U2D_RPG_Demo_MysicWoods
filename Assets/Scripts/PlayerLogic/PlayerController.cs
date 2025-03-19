using PlayerLogic;
using System;
using UnityEngine;
using static Argo_Utils.Utils;

public class PlayerController : MonoBehaviour {
    // Singleton instance for global access
    public static PlayerController Instance { get; private set; }

    #region Component Attributes
    [Header("Testing Attributes")]
    [SerializeField] public bool consoleLogOn;           // Enable/disable debug logging
    [SerializeField] private float smoothingFactor = 10f;  // Lerp smoothing factor for movement and look direction

    [Header("Base Attributes")]
    [SerializeField] private GameInput gameInput;          // Reference to the GameInput component
    [SerializeField] private PlayerStatus playerStatus;     // Reference to the PlayerStatus component

    [Header("Initialize Attributes")]
    [SerializeField] private ControlDirection InitDir;      // (Optional) initial direction (not used in current code)
    [SerializeField] private Vector2 InitMoveDir;           // (Optional) initial movement direction (not used in current code)
    #endregion

    #region Player Attributes and Private Fields
    // Define the possible states for the player
    public enum State { Idling, Moving, Running, Attacking, Dodge, Dead }

    private Rigidbody2D rb;                        // Player's Rigidbody2D for physics-based movement
    private Camera mainCamera;                     // Main Camera in the scene
    private AnimatorStateMachine playerStateMachine; // Custom state machine for handling player states

    private State currentState;                    // Current state of the player
    private Vector2 mouseLookDirection;            // Smoothed look direction based on mouse position
    private Vector2 moveDirection;                 // Movement vector from player input
    private ControlDirection enumDirection;        // Enum representing the current movement direction

    #endregion

    private void Awake() {
        // Check if there's already an instance, then assign this instance
        if (Instance != null) {
            DebugLog(consoleLogOn, "There is more than one Player instance");
        }
        Instance = this;

        // Get Rigidbody2D and main camera references
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        if (rb == null || mainCamera == null) return;

        // Initialize custom state machine
        playerStateMachine = new AnimatorStateMachine();
    }

    private void Start() {

    }

    private void Update() {
        // Update player movement and control direction based on input
        GetPlayerDirection();
    }

    private void FixedUpdate() {
        Movement();
        PlayerLookDirection();
        DebugLog(consoleLogOn, $"{GetPlayerCurrentPosition()}");
    }

    /// <summary>
    /// Handles player movement using physics (Rigidbody2D).
    /// Lerp is used for smoothing velocity changes.
    /// </summary>
    private void Movement() {
        // Calculate the target velocity based on move direction and player speed
        Vector2 targetVelocity = moveDirection * playerStatus.attributes.Speed;
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
    /// Determines the movement direction (as a ControlDirection enum) from the input vector.
    /// If the input vector is zero, returns the last known direction.
    /// </summary>
    /// <param name="direction">Input movement vector.</param>
    /// <returns>A ControlDirection indicating the dominant direction.</returns>
    public ControlDirection GetMoveDirection(Vector2 direction) {
        if (direction == Vector2.zero)
            return enumDirection;

        // Compare absolute x and y values to determine dominant movement direction
        return Mathf.Abs(direction.x) > Mathf.Abs(direction.y)
            ? (direction.x > 0 ? ControlDirection.Right : ControlDirection.Left)
            : (direction.y > 0 ? ControlDirection.Up : ControlDirection.Down);
    }

    /// <summary>
    /// Returns the normalized facing direction based on the current enum direction.
    /// </summary>
    /// <returns>A normalized Vector2 representing the facing direction.</returns>
    public Vector2 GetFacingDir() {
        return enumDirection switch {
            ControlDirection.Down => Vector2.down,
            ControlDirection.Up => Vector2.up,
            ControlDirection.Left => Vector2.left,
            ControlDirection.Right => Vector2.right,
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
    public State GetState() => currentState;

    /// <summary>
    /// Returns the current ControlDirection enum.
    /// </summary>
    public ControlDirection GetEnumDirection() => enumDirection;

    /// <summary>
    /// Returns the current move direction vector.
    /// </summary>
    public Vector2 GetMoveDirection() => moveDirection;

    /// <summary>
    /// Sets the player's current state.
    /// </summary>
    /// <param name="newState">New state to set.</param>
    public void SetCurrentState(State newState) => currentState = newState; 
}
