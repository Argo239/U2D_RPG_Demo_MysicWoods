using System;
using Interface;
using UnityEngine;
using static Argo_Utils.Utils;

public class PlayerMove : MonoBehaviour, IAction {
    public static PlayerMove Instance { get; private set; }

    private GameInput _gameInput;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveDir;
    private float _moveSpeed;
    private bool _isMoving;


    private float InitialSpeed = 4.0f;

    private void Awake() {
        if(Instance == null) Instance = this;

        _gameInput = PlayerController.Instance.gameInput;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _moveSpeed = 3.0f;
        
        _gameInput.OnPlayerRunning += GameInput_OnPlayerRunning;
        _gameInput.OnPlayerCancelRun += GameInput_OnPlayerCancelRun;
    }

    private void Update() {
        
    }

    private void GameInput_OnPlayerRunning(object sender, EventArgs e) {
        ToggleSpeedUp(true, InitialSpeed);
    }

    private void GameInput_OnPlayerCancelRun(object sender, EventArgs e) {
        ToggleSpeedUp(false, InitialSpeed);
    }

    private void HandleMovement(Vector2 inputVector) {
        _moveDir = new Vector2(inputVector.x, inputVector.y);
        _rigidbody2D.velocity = _moveDir * _moveSpeed;
        _isMoving = _moveDir != Vector2.zero;
    }

    public void ToggleSpeedUp(bool isSpeedingUp, float initialSpeed) {
        _moveSpeed = isSpeedingUp ? initialSpeed * 2f : initialSpeed;
    }

    public void Execute() {
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        HandleMovement(inputVector);
    }

    public void Stop() {
        _rigidbody2D.velocity = Vector2.zero;
    }
    
    public bool IsMoving() { return _isMoving; }
    public Vector2 GetMoveDir() { return _moveDir; }
}
