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
    private const string ISDODGE = "IsDodge";


    public static readonly int HorizontalMovement = Animator.StringToHash(HORIZONTAL);
    public static readonly int VerticalMovement = Animator.StringToHash(VERTICAL);
    public static readonly int AttackCount = Animator.StringToHash(ATTACKCOUNT);
    public static readonly int IsMove = Animator.StringToHash(ISMOVE);
    public static readonly int IsAttack = Animator.StringToHash(ISATTACK);
    public static readonly int IsDead = Animator.StringToHash(ISDEAD);
    public static readonly int IsRun = Animator.StringToHash(ISRUN);
    public static readonly int IsDodge = Animator.StringToHash(ISDODGE);
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
    private Animator _animator;
    private Skeleton _skeleton;
    private SkeletonMecanim _skeletonMecanim;
    private AnimatorStateMachine _playerStateMachine;
    private GameInput _gameInput;
    private PlayerController _playerController;
    private PlayerAttack _playerAttack;
    private PlayerMove _playerMove;
    private Vector2 _inputVector;
    private ControllDirection _cardinalDir;
    private Dictionary<string, SkeletonDataAsset> _skeletonDataAssets;
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
        _playerStateMachine = new AnimatorStateMachine(this);

        // Retrieve required components
        _animator = GetComponent<Animator>();
        _animator.updateMode = AnimatorUpdateMode.Fixed;

        _skeletonMecanim = GetComponent<SkeletonMecanim>();
        _skeletonMecanim.Initialize(true, true);

        // Populate the _skeleton data asset dictionary
        _skeletonDataAssets = new Dictionary<string, SkeletonDataAsset> {
            { frontSkeletonDataAsset.name, frontSkeletonDataAsset },
            { backSkeletonDataAsset.name, backSkeletonDataAsset },
            { sideSkeletonDataAsset.name, sideSkeletonDataAsset }
        };

        _playerController = PlayerController.Instance;
        _gameInput = _playerController.GameInput;
        _playerMove = _playerController.PlayerMove;
        _playerAttack = _playerController.PlayerAttack;

    }

    private void OnEnable() {
        //PlayerMove event
        _playerMove.OnPlayerStartMoving += PlayerMove_OnPlayerStartMoving;
        _playerMove.OnPlayerStopMoving += PlayerMove_OnPlayerStopMoving;
        _playerMove.OnPlayerStartRunning += PlayerMove_OnPlayerStartRunning;
        _playerMove.OnPlayerStopRunning += PlayerMove_OnPlayerStopRunning;
        _playerMove.OnPlayerDodgePerformed += PlayerMove_OnPlayerDodgePerformed;

        //PlayerAttack event
        _playerAttack.OnPlayerAttackPerformed += PlayerAttack_OnPlayerAttacking;
        _gameInput.OnPlayerRevive += GameInput_OnPlayerRevive;
        PlayerStatus.Instance.OnPlayerDeath += PlayerStatus_OnPlayerDeath;
    }


    private void OnDisable() {
        _playerMove.OnPlayerStartMoving -= PlayerMove_OnPlayerStartMoving;
        _playerMove.OnPlayerStopMoving -= PlayerMove_OnPlayerStopMoving;
        _playerMove.OnPlayerStartRunning -= PlayerMove_OnPlayerStartRunning;
        _playerMove.OnPlayerStopRunning -= PlayerMove_OnPlayerStopRunning;
        _playerMove.OnPlayerDodgePerformed -= PlayerMove_OnPlayerDodgePerformed;

        _playerAttack.OnPlayerAttackPerformed -= PlayerAttack_OnPlayerAttacking;
       
        _gameInput.OnPlayerRevive -= GameInput_OnPlayerRevive;
        PlayerStatus.Instance.OnPlayerDeath -= PlayerStatus_OnPlayerDeath;
    }

    /// <summary>
    /// Updates the player's movement _cardinalDir and passes it to the state machine.
    /// </summary>
    private void Update() {
        _inputVector = _playerController.GetInputVector();
        _cardinalDir = _playerController.GetCardinalDir();

        _playerStateMachine.Update(_cardinalDir, _inputVector);
    }

    #region Event Handlers

    private void PlayerMove_OnPlayerStartMoving(object sender, EventArgs e) {
        // Check if thse sprint/dodge key (Shift) is held
        if (_gameInput.IsSprintDodgeKeyHeld()) {
            // If already in RunState, update it; otherwise, switch to RunState
            if (_playerStateMachine.CurrentState is RunState) {
                _playerStateMachine.Update(_cardinalDir, _inputVector);
            } else {
                _playerStateMachine.ChangeState(new RunState(this), _cardinalDir, _inputVector);
            }
        } else {
            // If Shift is not held, switch to WalkState
            _playerStateMachine.ChangeState(new WalkState(this), _cardinalDir, _inputVector);
        }
    }

    private void PlayerMove_OnPlayerStopMoving(object sender, EventArgs e) =>
        _playerStateMachine.ChangeState(new IdleState(this), _cardinalDir, _inputVector);

    private void PlayerMove_OnPlayerStartRunning(object sender, EventArgs e) {
        if (_playerController.GetCurrentState() == PlayerController.State.Moving) {
            if (_playerStateMachine.CurrentState is RunState) {
                _playerStateMachine.Update(_cardinalDir, _inputVector);
            } else {
                _playerStateMachine.ChangeState(new RunState(this), _cardinalDir, _inputVector);
            }
        } else if (_playerController.GetCurrentState() == PlayerController.State.Attacking) {
            _playerStateMachine.ChangeState(new DodgeState(this), _cardinalDir, _inputVector);
        } else {
            _playerStateMachine.ChangeState(new IdleState(this), _cardinalDir, _inputVector);
        }

    }

    private void PlayerMove_OnPlayerStopRunning(object sender, EventArgs e) {
        var currentState = _playerController.GetCurrentState();
        if (currentState == PlayerController.State.Moving || currentState == PlayerController.State.Running) {
            _playerStateMachine.ChangeState(new WalkState(this), _cardinalDir, _inputVector);
        }
    }

    private void PlayerMove_OnPlayerDodgePerformed(object sender, EventArgs e) {
        if (!_playerController.CanDodge()) return;
        _playerStateMachine.ChangeState(new DodgeState(this), _cardinalDir, _inputVector, true);
    }

    private void PlayerAttack_OnPlayerAttacking(object sender, AttackEventArgs e) {
        var stepData = e.StepData;
        if (!(_playerStateMachine.CurrentState is AttackState)) {
            _playerStateMachine.ChangeState(new AttackState(this, _playerAttack, stepData), _cardinalDir, _inputVector, true); // Force transition if not already attacking
        }
    }

    private void GameInput_OnPlayerRevive(object sender, EventArgs e) =>
        _playerStateMachine.ChangeState(new IdleState(this), _cardinalDir, _inputVector);

    private void PlayerStatus_OnPlayerDeath(object sender, EventArgs e) =>
        _playerStateMachine.ChangeState(new DeadState(this), _cardinalDir, _inputVector);

    #endregion

    /// <summary>
    /// Attempts to update the player's tag state, switching _skeleton assets if necessary.
    /// </summary>
    public void TryToSetAnimation(int animatorID, bool value, ControllDirection cardinalDir, Vector2 InputVector) {
        string skeletonDataAssetKey = GetSkeletonDataAssetKey(cardinalDir);
        if (string.IsNullOrEmpty(skeletonDataAssetKey)) return;

        // Update _skeleton asset if needed
        SetSkeletonDataAsset(skeletonDataAssetKey);
        InitializedSkeletonDataAsset();
        SetAnimatorDirection(InputVector);
        SetAnimatorBool(animatorID, value);

        // Flip the _skeleton based on _cardinalDir
        _skeleton.ScaleX = cardinalDir == ControllDirection.Left ? -1f : 1f;
    }

    /// <summary>
    /// Retrieves the appropriate _skeleton data asset key based on the player's _cardinalDir.
    /// </summary>
    private string GetSkeletonDataAssetKey(ControllDirection cardinalDir) {
        return cardinalDir switch {
            ControllDirection.Up => backSkeletonDataAsset.name,
            ControllDirection.Down => frontSkeletonDataAsset.name,
            ControllDirection.Left => sideSkeletonDataAsset.name,
            ControllDirection.Right => sideSkeletonDataAsset.name,
            _ => null
        };
    }

    /// <summary>
    /// Returns the _animator state machine instance.
    /// </summary>
    public AnimatorStateMachine GetStateMachine() => _playerStateMachine;

    /// <summary>
    /// Retrieves a boolean value from the _animator.
    /// </summary>
    public bool GetAnimatorBool(int animatorID) => this._animator.GetBool(animatorID);

    /// <summary>
    /// Sets the _animator's movement parameters to influence tag _cardinalDir.
    /// </summary>
    private void SetAnimatorDirection(Vector2 playerLookDir) {
        _animator.SetFloat(HorizontalMovement, playerLookDir.x);
        _animator.SetFloat(VerticalMovement, playerLookDir.y);
    }

    /// <summary>
    /// Changes the _skeleton data asset if it is different from the current one.
    /// </summary>
    private void SetSkeletonDataAsset(string skeletonDataAssetKey) {
        if (!_skeletonDataAssets.TryGetValue(skeletonDataAssetKey, out var asset)) return;
        if (_skeletonMecanim.skeletonDataAsset != asset) {
            _skeletonMecanim.skeletonDataAsset = asset;
            _skeletonMecanim.Initialize(true);
        }
    }

    /// <summary>
    /// Sets a boolean parameter in the _animator.
    /// </summary>
    public void SetAnimatorBool(int animatorID, bool value) => this._animator.SetBool(animatorID, value);

    /// <summary>
    /// Sets an integer parameter in the _animator.
    /// </summary>
    public void SetAnimatorInt(int animatorID, int value) => this._animator.SetInteger(animatorID, value);

    /// <summary>
    /// Initializes the _skeleton data asset reference.
    /// </summary>
    private void InitializedSkeletonDataAsset() => _skeleton = _skeletonMecanim.skeleton;

    /// <summary>
    /// This method checks if a specified tag has finished playing
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="FinishTime"></param>
    /// <returns></returns>
    public bool CheckAnimationFinishedByTag(string tag, float FinishTime = 1f) {
        var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsTag(tag) && stateInfo.normalizedTime >= 1f;
    }
    
    /// <summary>
    /// This method checks if a specified tag has finished playing
    /// </summary>
    /// <param name="animationName"></param>
    /// <param name="FinishTime"></param>
    /// <returns></returns>
    public bool CheckAnimationFinishedByName(string animationName, float FinishTime = 1f) {
        var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1f;
    }
}
