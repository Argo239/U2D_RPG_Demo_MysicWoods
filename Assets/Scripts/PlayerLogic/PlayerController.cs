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
    [SerializeField] private float speed = 5f;
    [SerializeField] private float smoothingFactor = 10f;

    [Header("Base attributes")]
    [SerializeField] private GameInput gameInput;
    [SerializeField] private PlayerAnimator playerAnimator;
    #endregion

    #region Player attribute
    public enum State { Idling, Moving, Dead }

    private Rigidbody2D rb;
    private Camera mainCamera;
    private AnimatorStateMachine playerStateMachine;

    private State currentState;
    private Vector2 currentLookDirection;
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

        playerStateMachine.ChangeState(new IdleState(playerAnimator), enumDirection, currentLookDirection);
    }

    private void OnDestroy() {
        gameInput.OnPlayerMoving -= GameInput_OnPlayerMoving;
        gameInput.OnPlayerMoveCanceled -= GameInput_OnPlayerMoveCanceled;
    }

    private void Update() {
        Movement();
        UpdateCurrentDirection();
        PlayerLookDirection();
        playerStateMachine.Update(enumDirection, currentLookDirection);

        LogMessage(consoleLogOn, $"{currentState.ToString()}");
    }

    private void GameInput_OnPlayerMoving(object sender, EventArgs e) => playerStateMachine.ChangeState(new MoveState(playerAnimator), enumDirection, currentLookDirection);
    private void GameInput_OnPlayerMoveCanceled(object sender, EventArgs e) => playerStateMachine.ChangeState(new IdleState(playerAnimator), enumDirection, currentLookDirection);

    private void Movement() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector2 targetVelocity = new Vector2(inputVector.x, inputVector.y) * speed;
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, smoothingFactor * Time.deltaTime);
    }

    private Vector2 PlayerLookDirection() {
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

    public void UpdateCurrentDirection() { enumDirection = GetMoveDirection(); }

    public Vector2 PlayerCurrentPosition() { return transform.position; }

    public State GetState() { return currentState; }  

    public void SetCurrentState(State newState) { currentState = newState; }
}