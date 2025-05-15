using UnityEngine;
using static Argo_Utils.Utils;

public class PlayerController : MonoBehaviour {
    // Singleton instance for global access
    public static PlayerController Instance { get; private set; }
    public enum State { Idling, Moving, Running, Attacking, Dodge, Dead }

    #region Component Attributes
    [Header("Testing Attributes")]
    [SerializeField] public bool consoleLogOn;           // Enables/disables debug logging

    [Header("Base Attributes")]
    [SerializeField] internal GameInput GameInput;          // Reference to the GameInput component
    [SerializeField] private PlayerStatus playerStatus;     // Reference to the PlayerStatus component

    [Header("Initialize Attributes")]
    [SerializeField] private ControllDirection _initDir;     // (Optional) initial direction (unused)
    [SerializeField] private Vector2 _initMoveDir;           // (Optional) initial movement direction (unused)
    #endregion

    #region Player Attributes and Private Fields
    // Define possible states for the player

    internal PlayerMove PlayerMove;                   // Reference to the PlayerMove component
    internal PlayerAttack PlayerAttack;

    private Camera mainCamera;                       // Reference to the main camera
    private Vector2 inputVector;                   // Current movement vector (from input)
    private Vector2 facingDirection;                 // Current facing direction (smoothed from mouse)
    private State currentState;                      // Current state of the player
    private ControllDirection cardinalDir;         // Direction as an enum

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

        PlayerMove = gameObject.AddComponent<PlayerMove>();
        PlayerAttack = gameObject.AddComponent<PlayerAttack>();

    }

    private void Start() {

    }

    private void Update() {
        refresh();
    }

    private void refresh() {
        inputVector = PlayerMove.GetInputVector();
        cardinalDir = PlayerMove.GetCardinalDir();
        currentState = PlayerMove.GetCurrentState();
        facingDirection = PlayerMove.GetFacingVector();
    }

    /// <summary>
    /// Returns the player's current world position.
    /// </summary>
    /// <returns>Player position as a Vector2.</returns>
    public Vector2 GetPlayerCurrentPosition() => transform.position;

    /// <summary>
    /// Returns the current move direction vector.
    /// </summary>
    public Vector2 GetInputVector() => inputVector;

    /// <summary>
    /// Gets the player's current state.
    /// </summary>
    public PlayerController.State GetCurrentState() => currentState;

    /// <summary>
    /// Returns the current ControllDirection enum.
    /// </summary>
    public ControllDirection GetCardinalDir() => cardinalDir;

    /// <summary>
    /// Returns the normalized facing direction based on the current enum direction.
    /// </summary>
    /// <returns>A normalized Vector2 representing the facing direction.</returns>
    public Vector2 GetFacingDir() => facingDirection;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool CanDodge() => PlayerMove.canDodge;

    /// <summary>
    /// Sets the player's current state.
    /// </summary>
    /// <param name="newState">New state to set.</param>
    public void SetCurrentState(PlayerController.State newState) => PlayerMove.SetCurrentState(newState);
}
