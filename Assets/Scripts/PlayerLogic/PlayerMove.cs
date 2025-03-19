//using System;
//using Assets.Scripts.Attributes.AttributesComponent;
//using Interface;
//using Unity.VisualScripting;
//using UnityEngine;
//using static Argo_Utils.Utils;

//public class PlayerMove : MonoBehaviour, IAction {
//    public static PlayerMove Instance { get; private set; }

//    private GameInput _gameInput;
//    private PlayerAttributesComponent _playerAttributesComponent;
//    private Rigidbody2D _rigidbody2D;
//    private Vector2 _moveDir;
//    private float _moveSpeed;
//    private bool _isMoving;
//    private bool _isRunning;

//    private void Awake() {
//        if(Instance == null) Instance = this;
//        _rigidbody2D = GetComponent<Rigidbody2D>();
//    }

//    private void Start() {
//        _gameInput = PlayerController.Instance.GetGameInput();
//        _playerAttributesComponent = gameObject.GetComponent<PlayerAttributesComponent>();
//        UpdateMoveSpeed();
//    }

//    private void OnDestroy() {

//    }

//    public void Execute() {
//        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
//        UpdateMoveSpeed();
//        HandleMovement(inputVector);
//    }

//    public void Stop() { _rigidbody2D.linearVelocity = Vector2.zero; }

//    private void HandleMovement(Vector2 inputVector) {
//        _moveDir = new Vector2(inputVector.x, inputVector.y);
//        _rigidbody2D.linearVelocity = _moveDir * _moveSpeed;
//        _isMoving = _moveDir != Vector2.zero;
//    }

//    public void UpdateMoveSpeed() {
//        _moveSpeed = _playerAttributesComponent.PlayerAttributes.SpeedAttribute.GetSpeed(_isRunning);
//    }

//    public bool IsMove() => _isMoving;
//    public Vector2 GetMoveDir() => _moveDir;
//}
