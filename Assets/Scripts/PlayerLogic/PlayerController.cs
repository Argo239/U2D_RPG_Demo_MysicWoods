using PlayerLogic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using static Argo_Utils.Utils;

public class PlayerController : MonoBehaviour {
    public static PlayerController Instance { get; private set; }

    #region Component attributes

    [Header("Testing attributes")]
    [SerializeField] public bool consoleLogOn;
    [SerializeField] private float smoothingFactor = 10f;

    [Header("Base attributes")]
    [SerializeField] private GameInput gameInput;
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private PlayerStatus playerStatus;
    #endregion

    #region Player attribute
    public enum State { Idling, Moving, Dead }

    private Rigidbody2D rb;
    private Camera mainCamera;
    private AnimatorStateMachine playerStateMachine;

    private State currentState;
    private Vector2 mouseLookDirection;
    private Vector2 moveDirection;
    private MoveDirection enumDirection;
    #endregion

    private void Awake() {
        if (Instance != null) LogMessage(consoleLogOn, "There is more than one Player instance");
        Instance = this;

        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        if (rb == null || mainCamera == null) return;

        playerStateMachine = new AnimatorStateMachine();

    }

    private void Start() {
        gameInput.OnPlayerMoving += GameInput_OnPlayerMoving;
        gameInput.OnPlayerMoveCanceled += GameInput_OnPlayerMoveCanceled;
        gameInput.OnPlayerRevive += GameInput_OnPlayerRevive;
        playerStatus.OnPlayerDeath += PlayerStatus_OnPlayerDeath;

        playerStateMachine.ChangeState(new IdleState(playerAnimator), enumDirection, moveDirection);
    }

    private void OnDestroy() {
        gameInput.OnPlayerMoving -= GameInput_OnPlayerMoving;
        gameInput.OnPlayerMoveCanceled -= GameInput_OnPlayerMoveCanceled;
    }

    private void Update() {
        GetPlayerDirection();
        playerStateMachine.Update(enumDirection, moveDirection);
    }

    private void FixedUpdate() {
        Movement();
        PlayerLookDirection();

        LogMessage(consoleLogOn, $"{PlayerCurrentPosition()}");
    }

    private void GameInput_OnPlayerMoving(object sender, EventArgs e) => playerStateMachine.ChangeState(new MoveState(playerAnimator), enumDirection, moveDirection);
    private void GameInput_OnPlayerMoveCanceled(object sender, EventArgs e) => playerStateMachine.ChangeState(new IdleState(playerAnimator), enumDirection, moveDirection);
    private void GameInput_OnPlayerRevive(object sender, EventArgs e) => playerStateMachine.ChangeState(new IdleState(playerAnimator), enumDirection, moveDirection);
    private void PlayerStatus_OnPlayerDeath(object sender, EventArgs e) => playerStateMachine.ChangeState(new DeadState(playerAnimator), enumDirection, moveDirection);

    private void Movement() {
        Vector2 targetVelocity = new Vector2(moveDirection.x, moveDirection.y) * playerStatus.attributes.Speed;
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, smoothingFactor * Time.deltaTime);

        //Vector2 targetPosition = new Vector2(transform.position.x + moveDirection.x * playerStatus.attributes.Speed * Time.deltaTime, transform.position.y + moveDirection.y * playerStatus.attributes.Speed * Time.deltaTime);
        //transform.position = Vector2.Lerp(transform.position, targetPosition, smoothingFactor * Time.deltaTime); // 使用 Lerp 来平滑过渡
    }

    private Vector2 PlayerLookDirection() {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        Vector2 playerPosition = PlayerCurrentPosition();
        Vector2 targetDirection = (worldMousePosition - playerPosition).normalized;

        mouseLookDirection = Vector2.Lerp(mouseLookDirection, targetDirection, smoothingFactor * Time.deltaTime);
        return mouseLookDirection;
    }

    /// <summary>
    ///     <para>Get the Move Direction</para>
    /// </summary>
    /// <param name="moveDir"></param>
    /// <returns></returns>
    public MoveDirection GetMoveDirection(Vector2 direction) {
        if (direction == Vector2.zero) 
            return enumDirection;
        return Mathf.Abs(direction.x) > Mathf.Abs(direction.y) ? direction.x > 0 ? MoveDirection.Right : MoveDirection.Left :
        direction.y > 0 ? MoveDirection.Up : MoveDirection.Down;
    }

    public Vector2 PlayerCurrentPosition() { return transform.position; }

    public State GetState() { return currentState; }

    public void GetPlayerDirection(){ 
        moveDirection = gameInput.GetMovementVectorNormalized(); 
        enumDirection = GetMoveDirection(moveDirection);
    }

    public void SetCurrentState(State newState) { currentState = newState; }
}