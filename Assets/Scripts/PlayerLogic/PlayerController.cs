using UnityEngine;
using static Argo_Utils.Utils;

public class PlayerController : MonoBehaviour {
    // Singleton instance for global access
    public static PlayerController Instance { get; private set; }

    #region Component Attributes
    [Header("Testing Attributes")]
    [SerializeField] public bool consoleLogOn;           // Enables/disables debug logging
    [SerializeField] private float smoothingFactor = 10f;  // Lerp smoothing factor for movement and look direction

    [Header("Base Attributes")]
    [SerializeField] private GameInput gameInput;          // Reference to the GameInput component
    [SerializeField] private PlayerStatus playerStatus;     // Reference to the PlayerStatus component

    [Header("Initialize Attributes")]
    [SerializeField] private ControllDirection InitDir;     // (Optional) initial direction (unused)
    [SerializeField] private Vector2 InitMoveDir;           // (Optional) initial movement direction (unused)
    #endregion

    #region Player Attributes and Private Fields
    // Define possible states for the player
    public enum State { Idling, Moving, Running, Attacking, Dodge, Dead }

    private Camera mainCamera;                       // Reference to the main camera
    private PlayerMove playerMove;                   // Reference to the PlayerMove component

    private Vector2 moveDirection;                   // Current movement vector (from input)
    private Vector2 facingDirection;                 // Current facing direction (smoothed from mouse)
    private State currentState;                      // Current state of the player
    private ControllDirection enumDirection;         // Direction as an enum

    #endregion

    private void Awake() {
        // Check if there's already an instance, then assign this instance
        if (Instance != null) {
            DebugLog(consoleLogOn, "There is more than one Player instance");
        }
        Instance = this;

        // Get main camera references
        mainCamera = Camera.main;
        if (mainCamera == null) return;
    }

    private void Start() {
        playerMove = gameObject.AddComponent<PlayerMove>();
        
    }

    private void Update() {
        refresh();

        IsShiftHeld();
        DebugLog(consoleLogOn, playerMove.GetCurrentState());
    }

    private void FixedUpdate() {
    }

    private void IsShiftHeld() {
        if (gameInput.IsSprintDodgeKeyHeld()) DebugLog(consoleLogOn, $"shift holding");
    }

    private void refresh() {
        moveDirection = playerMove.GetMoveDirection();
        currentState = playerMove.GetCurrentState();
        enumDirection = playerMove.GetEnumDirection();
        facingDirection = playerMove.GetFacingDir();
    }

    /// <summary>
    /// Returns the player's current world position.
    /// </summary>
    /// <returns>Player position as a Vector2.</returns>
    public Vector2 GetPlayerCurrentPosition() => transform.position;

    /// <summary>
    /// Returns the current move direction vector.
    /// </summary>
    public Vector2 GetMoveDirection() => moveDirection;

    /// <summary>
    /// Gets the player's current state.
    /// </summary>
    public PlayerController.State GetCurrentState() => currentState;

    /// <summary>
    /// Returns the current ControllDirection enum.
    /// </summary>
    public ControllDirection GetEnumDirection() => enumDirection;

    /// <summary>
    /// Returns the normalized facing direction based on the current enum direction.
    /// </summary>
    /// <returns>A normalized Vector2 representing the facing direction.</returns>
    public Vector2 GetFacingDir() => facingDirection;

    /// <summary>
    /// Sets the player's current state.
    /// </summary>
    /// <param name="newState">New state to set.</param>
    public void SetCurrentState(PlayerController.State newState) => playerMove.SetCurrentState(newState);
}
