using System;
using Assets.Scripts.Attributes.AttributesComponent;
using Interface;
using Unity.VisualScripting;
using UnityEngine;
using static Argo_Utils.Utils;

public class PlayerMove : MonoBehaviour, IAction {
    public static PlayerMove Instance { get; private set; }

    private GameInput _gameInput;
    private PlayerAttributesComponent _playerAttributesComponent;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveDir;
    private float _moveSpeed;
    private bool _isMoving;
    private bool _isRunning;

    private void Awake() {
        if(Instance == null) Instance = this;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        _gameInput = PlayerController.Instance.GetGameInput();
        _playerAttributesComponent = gameObject.GetComponent<PlayerAttributesComponent>();
        _moveSpeed = _playerAttributesComponent.PlayerAttributes.SPD;
    }

    private void OnDestroy() {

    }

    public void Execute() {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        _isRunning = _gameInput.CheckRunnningState();
        ToggleSPD(_isRunning);
        HandleMovement(inputVector);
    }

    public void Stop() { _rigidbody2D.velocity = Vector2.zero; }

    private void HandleMovement(Vector2 inputVector) {
        _moveDir = new Vector2(inputVector.x, inputVector.y);
        _rigidbody2D.velocity = _moveDir * _moveSpeed;
        _isMoving = _moveDir != Vector2.zero;
    }

    public void ToggleSPD(bool isSPDUp) {
        _moveSpeed = isSPDUp ? _playerAttributesComponent.PlayerAttributes.SPD + _playerAttributesComponent.PlayerAttributes.SPD_MULT
            : _playerAttributesComponent.PlayerAttributes.SPD;
    }

    public bool IsMoving() => _isMoving;
    public bool GetRunningState() => _gameInput.CheckRunnningState();
    public void SetIsRunning(bool isRunning) => _isRunning = isRunning;
    public Vector2 GetMoveDir() => _moveDir;
}
