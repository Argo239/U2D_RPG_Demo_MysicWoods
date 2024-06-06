using System;
using UnityEngine;
using static Argo_Utils.Utils;

public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }
    
    private const string PLAYERLAYER = nameof(Player);

    public enum PlayerState { Attack, Idle, Moving }

    [SerializeField] private bool consoleMessage;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] public GameInput gameInput;

    private Rigidbody2D _rigidbody2D;
    private Vector3 _moveDir;
    public PlayerState state;
    private bool _isWalking;
    private bool _isSpeedingUp = false;
    private float _initialVelocity;

    private void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        if (Instance != null) {
            Debug.LogError("There is more than one Player instance");
        }
        if (_rigidbody2D == null) {
            Debug.LogError("dont have rigidbody");
        }
        if (gameInput == null) {
            Debug.LogError("dont have GameInput");
        }
        Instance = this;
    }

    private void Start()
    {
        state = PlayerState.Idle;
        _initialVelocity = moveSpeed;

        gameInput.OnPlayerMoving += GameInput_OnPlayerMoving;
        gameInput.OnPlayerRunning += GameInputOnPlayerRunning;
        gameInput.OnPlayerAttacking += GameInput_OnPlayerAttack;
    }

    private void GameInput_OnPlayerAttack(object sender, EventArgs e) {
    }

    private void GameInput_OnPlayerMoving(object sender, EventArgs e) {
        
    }

    private void Update() {
        HandleMovement();
        if(state != PlayerState.Attack) {
            state = (_moveDir == Vector3.zero) ? PlayerState.Idle : PlayerState.Moving;
        }
    }

    private void HandleMovement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        _moveDir = new Vector2(inputVector.x, inputVector.y);

        _rigidbody2D.velocity = _moveDir * moveSpeed;
         
        _isWalking = _moveDir != Vector3.zero;
    }

    private void GameInputOnPlayerRunning(object sender, EventArgs e) {
        _isSpeedingUp = !_isSpeedingUp;
        moveSpeed = _isSpeedingUp ? _initialVelocity * 2f : _initialVelocity;
    }
    
    /// <summary>
    /// Return player IsWalking
    /// </summary>
    /// <returns></returns>
    public bool IsWalking() { return _isWalking; }

    /// <summary>
    /// Return player IsSpeedingUp
    /// </summary>
    /// <returns></returns>
    public bool IsSpeedingUp() { return _isSpeedingUp; }

    /// <summary>
    /// Return player current move direction
    /// </summary>
    /// <returns>Move direction</returns>
    public Vector3 GetMoveDir() { return _moveDir; }

    /// <summary>
    /// Return player current state
    /// </summary>
    /// <returns>Player State</returns>
    public PlayerState GetPlayerState() { return state; }
}