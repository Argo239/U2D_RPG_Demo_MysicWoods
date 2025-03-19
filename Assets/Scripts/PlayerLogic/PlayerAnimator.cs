using PlayerLogic;
using Spine;
using Spine.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {
    // Singleton instance
    public static PlayerAnimator Instance { get; private set; }

    #region Animator Parameter Constants
    // Define parameter names and their corresponding hash values for the Animator
    private const string HORIZONTAL = "HorizontalMovement";
    private const string VERTICAL = "VerticalMovement";
    private const string ATTACKCOUNT = "AttackCount";
    private const string ISMOVE = "IsMove";
    private const string ISATTACK = "IsAttack";
    private const string ISDEAD = "IsDead";
    private const string ISRUN = "IsRun";
    private const string ISSPRINTDODGE = "IsSprintDodge";


    public static readonly int HorizontalMovement = Animator.StringToHash(HORIZONTAL);
    public static readonly int VerticalMovement = Animator.StringToHash(VERTICAL);
    public static readonly int AttackCount = Animator.StringToHash(ATTACKCOUNT);
    public static readonly int IsMove = Animator.StringToHash(ISMOVE);
    public static readonly int IsAttack = Animator.StringToHash(ISATTACK);
    public static readonly int IsDead = Animator.StringToHash(ISDEAD);
    public static readonly int IsRun = Animator.StringToHash(ISRUN);
    public static readonly int IsSprintDodge = Animator.StringToHash(ISSPRINTDODGE);
    #endregion

    #region Component Attributes (Serialized Fields)
    [Header("Debugging Options")]
    [SerializeField] private bool consoleLogOn;

    [Header("Skeleton Data Assets")]
    [SerializeField] private SkeletonDataAsset frontSkeletonDataAsset;
    [SerializeField] private SkeletonDataAsset backSkeletonDataAsset;
    [SerializeField] private SkeletonDataAsset sideSkeletonDataAsset;
    #endregion

    #region Private Fields
    private Animator animator;
    private Skeleton skeleton;
    private GameInput gameInput;
    private SkeletonMecanim skeletonMecanim;
    private PlayerController playerController;
    private AnimatorStateMachine playerStateMachine;
    private Vector2 moveDirection;
    private ControlDirection enumDirection;
    private Dictionary<string, SkeletonDataAsset> skeletonDataAssets;
    #endregion

    /// <summary>
    /// Initializes the singleton instance and required components.
    /// </summary>
    private void Awake() {
        // Ensure only one instance exists
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Initialize the state machine
        playerStateMachine = new AnimatorStateMachine();

        // Retrieve required components
        animator = GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.Fixed;

        skeletonMecanim = GetComponent<SkeletonMecanim>();
        skeletonMecanim.Initialize(true, true);

        // Populate the skeleton data asset dictionary
        skeletonDataAssets = new Dictionary<string, SkeletonDataAsset> {
            { frontSkeletonDataAsset.name, frontSkeletonDataAsset },
            { backSkeletonDataAsset.name, backSkeletonDataAsset },
            { sideSkeletonDataAsset.name, sideSkeletonDataAsset }
        };
    }

    /// <summary>
    /// Registers event listeners for player actions.
    /// </summary>
    private void Start() {
        playerController = PlayerController.Instance;
        gameInput = GameInput.Instance;

        // Subscribe to player input events
        gameInput.OnPlayerMoving += GameInput_OnPlayerMoving;
        gameInput.OnPlayerMoveCanceled += GameInput_OnPlayerMoveCanceled;
        gameInput.OnPlayerSprintDogePerformed += GameInput_OnPlayerSprintDogePerformed;
        gameInput.OnPlayerSprintFinished += GameInput_OnPlayerSprintFinished;
        gameInput.OnPlayerAttacking += GameInput_OnPlayerAttacking;
        gameInput.OnPlayerRevive += GameInput_OnPlayerRevive;
        PlayerStatus.Instance.OnPlayerDeath += PlayerStatus_OnPlayerDeath;
    }


    /// <summary>
    /// Unsubscribes from event listeners to prevent memory leaks.
    /// </summary>
    private void OnDestroy() {
        gameInput.OnPlayerMoving -= GameInput_OnPlayerMoving;
        gameInput.OnPlayerMoveCanceled -= GameInput_OnPlayerMoveCanceled;
        gameInput.OnPlayerSprintDogePerformed -= GameInput_OnPlayerSprintDogePerformed;
        gameInput.OnPlayerSprintFinished -= GameInput_OnPlayerSprintFinished;
        gameInput.OnPlayerAttacking -= GameInput_OnPlayerAttacking;
        gameInput.OnPlayerRevive -= GameInput_OnPlayerRevive;
        PlayerStatus.Instance.OnPlayerDeath -= PlayerStatus_OnPlayerDeath;
    }

    /// <summary>
    /// Updates the player's movement direction and passes it to the state machine.
    /// </summary>
    private void Update() {
        enumDirection = playerController.GetEnumDirection();
        moveDirection = playerController.GetMoveDirection();
        playerStateMachine.Update(enumDirection, moveDirection);
    }

    #region Event Handlers
    private void GameInput_OnPlayerMoving(object sender, EventArgs e) =>
        playerStateMachine.ChangeState(new MoveState(this), enumDirection, moveDirection);

    private void GameInput_OnPlayerMoveCanceled(object sender, EventArgs e) =>
        playerStateMachine.ChangeState(new IdleState(this), enumDirection, moveDirection);

    private void GameInput_OnPlayerAttacking(object sender, EventArgs e) =>
        playerStateMachine.ChangeState(new AttackState(this), enumDirection, moveDirection);

    private void GameInput_OnPlayerRevive(object sender, EventArgs e) =>
        playerStateMachine.ChangeState(new IdleState(this), enumDirection, moveDirection);

    private void PlayerStatus_OnPlayerDeath(object sender, EventArgs e) =>
        playerStateMachine.ChangeState(new DeadState(this), enumDirection, moveDirection);
    private void GameInput_OnPlayerSprintDogePerformed(object sender, EventArgs e) {
        throw new NotImplementedException();
    }

    private void GameInput_OnPlayerSprintFinished(object sender, EventArgs e) {
        throw new NotImplementedException();
    }
    #endregion

    /// <summary>
    /// Attempts to update the player's animation state, switching skeleton assets if necessary.
    /// </summary>
    public void TryToSetAnimation(int animatorID, bool value, ControlDirection direction, Vector2 playerLookDir) {
        string skeletonDataAssetKey = GetSkeletonDataAssetKey(direction);
        if (string.IsNullOrEmpty(skeletonDataAssetKey)) return;

        // Update skeleton asset if needed
        SetSkeletonDataAsset(skeletonDataAssetKey);
        InitializedSkeletonDataAsset();
        SetAnimatorDirection(playerLookDir);
        SetAnimatorBool(animatorID, value);

        // Flip the skeleton based on direction
        skeleton.ScaleX = direction == ControlDirection.Left ? -1f : 1f;
    }

    /// <summary>
    /// Retrieves the appropriate skeleton data asset key based on the player's direction.
    /// </summary>
    private string GetSkeletonDataAssetKey(ControlDirection direction) {
        return direction switch {
            ControlDirection.Up => backSkeletonDataAsset.name,
            ControlDirection.Down => frontSkeletonDataAsset.name,
            ControlDirection.Left => sideSkeletonDataAsset.name,
            ControlDirection.Right => sideSkeletonDataAsset.name,
            _ => null
        };
    }

    /// <summary>
    /// Returns the animator state machine instance.
    /// </summary>
    public AnimatorStateMachine GetStateMachine() => playerStateMachine;

    /// <summary>
    /// Retrieves a boolean value from the animator.
    /// </summary>
    public bool GetAnimatorBool(int animatorID) => this.animator.GetBool(animatorID);

    /// <summary>
    /// Sets the animator's movement parameters to influence animation direction.
    /// </summary>
    private void SetAnimatorDirection(Vector2 playerLookDir) {
        animator.SetFloat(HorizontalMovement, playerLookDir.x);
        animator.SetFloat(VerticalMovement, playerLookDir.y);
    }

    /// <summary>
    /// Changes the skeleton data asset if it is different from the current one.
    /// </summary>
    private void SetSkeletonDataAsset(string skeletonDataAssetKey) {
        if (!skeletonDataAssets.TryGetValue(skeletonDataAssetKey, out var asset)) return;
        if (skeletonMecanim.skeletonDataAsset != asset) {
            skeletonMecanim.skeletonDataAsset = asset;
            skeletonMecanim.Initialize(true);
        }
    }

    /// <summary>
    /// Sets a boolean parameter in the animator.
    /// </summary>
    public void SetAnimatorBool(int animatorID, bool value) => this.animator.SetBool(animatorID, value);

    /// <summary>
    /// Sets an integer parameter in the animator.
    /// </summary>
    public void SetAnimatorInt(int animatorID, int value) => this.animator.SetInteger(animatorID, value);

    /// <summary>
    /// Initializes the skeleton data asset reference.
    /// </summary>
    private void InitializedSkeletonDataAsset() => skeleton = skeletonMecanim.skeleton;

    /// <summary>
    /// Determines if the attack animation has completed (70% or more progress).
    /// </summary>
    public bool IsAttackAnimationFinished() {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        bool isInAttackEndState = stateInfo.IsName("rotation_attack");
        bool isAnimationFinished = stateInfo.normalizedTime >= 0.7f;
        return isInAttackEndState && isAnimationFinished;
    }
}
