using System;
using System.Collections.Generic;
using Interface;
using PlayerLogic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static Argo_Utils.Utils;

public class PlayerController : MonoBehaviour {
    public static PlayerController Instance { get; private set; }

    #region Component attributes

    [Header("Testing attributes")]
    [SerializeField] public bool consoleLogOn;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float smoothingFactor = 10f;

    [Header("Base attributes")]
    [SerializeField] private GameInput gameInput;
    #endregion

    #region Player attribute
    public enum State { Idling, Moving }

    private Rigidbody2D rb;
    private Camera mainCamera;
    private PlayerAnimator playerAnimator;
    private AnimatorStateMachine playerStateMachine;
    private Vector2 currentLookDirection;
    private MoveDirection enumDirection;
    #endregion

    private void Awake() {
        if (Instance != null) LogMessage(consoleLogOn, "There is more than one Player instance");
        Instance = this;

        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        if (rb == null || mainCamera == null) return;
    }

    private void Start() {
        gameInput.OnPlayerMoving += GameInput_OnPlayerMoving;
        gameInput.OnPlayerMoveCanceled += GameInput_OnPlayerMoveCanceled;

        playerAnimator = GetComponent<PlayerAnimator>();
        playerStateMachine = new AnimatorStateMachine();
        playerStateMachine.ChangeState(new IdleState(playerAnimator), enumDirection, currentLookDirection);
    }

    private void OnDestroy() {
        gameInput.OnPlayerMoving -= GameInput_OnPlayerMoving;
        gameInput.OnPlayerMoveCanceled -= GameInput_OnPlayerMoveCanceled;
    }

    private void Update() {
        Movement();
        UpdateCurrentDirection();
        playerLookPosition();
        playerStateMachine.Update(enumDirection, currentLookDirection);
    }

    private void GameInput_OnPlayerMoving(object sender, EventArgs e) {
        LogMessage(consoleLogOn, "Moving");
        playerStateMachine.ChangeState(new MoveState(playerAnimator), enumDirection, currentLookDirection);
    }

    private void GameInput_OnPlayerMoveCanceled(object sender, EventArgs e) {
        LogMessage(consoleLogOn, "Idling");
        playerStateMachine.ChangeState(new IdleState(playerAnimator), enumDirection, currentLookDirection);
    }

    private void Movement() {
        //float speed = 5f;

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector2 targetVelocity = new Vector2(inputVector.x, inputVector.y) * speed;
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, smoothingFactor * Time.deltaTime);
    }

    private Vector2 playerLookPosition() {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        Vector2 playerPosition = PlayerCurrentPosition();
        Vector2 targetDirection = (worldMousePosition - playerPosition).normalized;

        currentLookDirection = Vector2.Lerp(currentLookDirection, targetDirection, smoothingFactor * Time.deltaTime);

        return currentLookDirection;
    }

    /// <summary>
    ///     <para>Get the Move Direction</para>
    /// </summary>
    /// <param name="moveDir"></param>
    /// <returns></returns>
    public MoveDirection GetMoveDirection() {
        if (currentLookDirection == Vector2.zero) return MoveDirection.Down;
        return Mathf.Abs(currentLookDirection.x) > Mathf.Abs(currentLookDirection.y) ? currentLookDirection.x > 0 ? MoveDirection.Right : MoveDirection.Left :
            currentLookDirection.y > 0 ? MoveDirection.Up : MoveDirection.Down;
    }

    public void UpdateCurrentDirection() {
        enumDirection = GetMoveDirection();
    }

    public Vector2 PlayerCurrentPosition() {
        return transform.position;
    }
}



//private void Awake() {
//    _rigidbody2D = GetComponent<Rigidbody2D>();
//    _mainCamera = Camera.main;

//    if(Instance != null) LogMessage(consoleLogOn, "There is more than one Player instance");
//    if(_rigidbody2D == null) return;

//    Instance = this;
//    _currentState = State.Idling;

//    InitializePlayerActions();
//}

//private void InitializePlayerActions() {
//    _playerActions = new Dictionary<State, IAction> {
//        { State.Moving, gameObject.AddComponent<PlayerMove>() }
//    };
//}

//private void Start() {
//    gameInput.OnPlayerMoveCanceled += GameInput_OnPlayerIdling;
//    gameInput.OnPlayerMoving += GameInput_OnPlayerMoving;
//}

//private void OnDestroy() {
//    if (gameInput == null) return;
//    gameInput.OnPlayerMoveCanceled -= GameInput_OnPlayerIdling;
//    gameInput.OnPlayerMoving -= GameInput_OnPlayerMoving;
//}

//private void Update() {
//    if (_playerActions.ContainsKey(_currentState)) {
//        _playerActions[_currentState].Execute(); 
//    }
//}

//private void GameInput_OnPlayerIdling(object sender, EventArgs e) => SetState(State.Idling);

//private void GameInput_OnPlayerMoving(object sender, EventArgs e) {
//    SetState(State.Moving);
//}


//private void SetState(State newState) {
//    if (_currentState == newState) return; 
//    if (_playerActions.ContainsKey(_currentState)) 
//        _playerActions[_currentState].Stop();
//    _currentState = newState;
//}

//public void SetStateIdle() => SetState(State.Idling);
//public State GetCurrentState() => _currentState;
//public GameInput GetGameInput() => gameInput;
