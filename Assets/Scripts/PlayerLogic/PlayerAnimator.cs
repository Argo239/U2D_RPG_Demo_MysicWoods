using System;
using System.Collections.Generic;
using PlayerLogic;
using Spine;
using Spine.Unity;
using UnityEngine;
using static Argo_Utils.Utils;

public class PlayerAnimator : MonoBehaviour {
    public static PlayerAnimator Instance { get; private set; }
    
    
    #region Animator attribute

    //Float
    private const string HORIZONTAL = "HorizontalMovement";
    private const string VERTICAL = "VerticalMovement";
    
    //Boolean
    private const string ISMOVING = "IsMoving";
    private const string ISATTACKING = "IsAttacking";
    
    public static readonly int IsMoving = Animator.StringToHash(ISMOVING);
    public static readonly int IsAttacking = Animator.StringToHash(ISATTACKING);
    public static readonly int HorizontalMovement = Animator.StringToHash(HORIZONTAL);
    public static readonly int VerticalMovement = Animator.StringToHash(VERTICAL);

    #endregion

    
    #region Component attributes
    
    [Header("Testing attribute")]
    [SerializeField] private bool consoleLogOn;

    [Header("Base attribute")]
    [SerializeField] private PlayerController playerController;

    [Header("Skeleton attribute")] 
    [SerializeField] private SkeletonDataAsset frontSkeletonDataAsset;
    [SerializeField] private SkeletonDataAsset backSkeletonDataAsset;
    [SerializeField] private SkeletonDataAsset sideSkeletonDataAsset;

    #endregion

    
    #region Attribute

    private enum MoveDirection { None, Up, Down, Left, Right }

    private PlayerMove _playerMove;
    private Animator _animator;
    private GameInput _gameInput;
    private Skeleton _skeleton;
    private SkeletonMecanim _skeletonMecanim;
    private Dictionary<string, SkeletonDataAsset> _skeletonDataAssets;

    private PlayerStateMachine _stateMachine;
    private Vector3 _playerMoveDir;
    
    #endregion

    
    private void Awake() {
        Instance = this;
        _playerMove = PlayerMove.Instance;
        _animator = GetComponent<Animator>();
        _skeletonMecanim = GetComponent<SkeletonMecanim>();
        
        _gameInput = playerController.gameInput;
        _skeleton = _skeletonMecanim.skeleton;

        _skeletonDataAssets = new Dictionary<string, SkeletonDataAsset> {
            { frontSkeletonDataAsset.name, frontSkeletonDataAsset },
            { backSkeletonDataAsset.name, backSkeletonDataAsset },
            { sideSkeletonDataAsset.name, sideSkeletonDataAsset }
        };

        _stateMachine = new PlayerStateMachine();
        _stateMachine.ChangeState(new IdleState(), this);
        
        _gameInput.OnPlayerMoving += GameInput_OnPlayerMoving;
        _gameInput.OnPlayerRunning += GameInput_OnPlayerRunning;
        _gameInput.OnPlayerAttack += GameInput_OnPlayerAttack;
        _gameInput.OnPlayerCancelRun += GameInput_OnPlayerCancelRun;
    }

    private void OnDestroy() {
        _gameInput.OnPlayerMoving -= GameInput_OnPlayerMoving;
        _gameInput.OnPlayerRunning -= GameInput_OnPlayerRunning;
        _gameInput.OnPlayerAttack -= GameInput_OnPlayerAttack;
        _gameInput.OnPlayerCancelRun -= GameInput_OnPlayerCancelRun;
    }

    private void GameInput_OnPlayerMoving(object sender, EventArgs e) {
        _stateMachine.ChangeState(new MovingState(), this);
    }

    private void GameInput_OnPlayerRunning(object sender, EventArgs e) {
        _stateMachine.ChangeState(new RunningState(), this);
    }

    private void GameInput_OnPlayerAttack(object sender, EventArgs e) {
        _stateMachine.ChangeState(new AttackingState(), this);
    }

    private void GameInput_OnPlayerCancelRun(object sender, EventArgs e) {
        _stateMachine.ChangeState(new IdleState(), this);
    }
    
    private void Update() {
        _stateMachine.Update(this);
        LogMessage(consoleLogOn, _playerMoveDir);
    }

    public void UpdateMoveDirection() {
        if(_playerMove is null) return;
        _playerMoveDir = _playerMove.GetMoveDir();
    }

    public void TryToSetAnimation(int animatorId, bool value) {
        MoveDirection direction = GetMoveDirection(_playerMoveDir);
        string skeletonDataAssetKey = GetSkeletonDataAssetKey(direction);
        
        if (string.IsNullOrEmpty(skeletonDataAssetKey)) return;
    
        SetSkeletonDataAsset(skeletonDataAssetKey);
        InitializedSkeletonDataAsset();
        SetAnimatorDirection();
        
        _animator.SetBool(animatorId, value);
        Flip(direction == MoveDirection.Left);
    }
    
    /// <summary>
    ///     <para>Get the Move Direction</para>
    /// </summary>
    /// <param name="moveDir"></param>
    /// <returns></returns>
    private MoveDirection GetMoveDirection(Vector2 moveDir) {
        if (moveDir == Vector2.zero) return MoveDirection.None;
        return Mathf.Abs(moveDir.x) > Mathf.Abs(moveDir.y) ? moveDir.x > 0 ? MoveDirection.Right : MoveDirection.Left :
            moveDir.y > 0 ? MoveDirection.Up : MoveDirection.Down;
    }
    
    /// <summary>
    /// <para> Key to get skeleton data asset </para>
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private string GetSkeletonDataAssetKey(MoveDirection direction) {
        return direction switch {
            MoveDirection.Up => backSkeletonDataAsset.name,
            MoveDirection.Down => frontSkeletonDataAsset.name,
            MoveDirection.Left => sideSkeletonDataAsset.name,
            MoveDirection.Right => sideSkeletonDataAsset.name,
            _ => null
        };
    }
    
    /// <summary>
    /// <para> Set the animator Direction </para>
    /// </summary>
    private void SetAnimatorDirection() {
        _animator.SetFloat(HorizontalMovement, _playerMoveDir.x);
        _animator.SetFloat(VerticalMovement, _playerMoveDir.y);
    }
    
    /// <summary>
    /// Change the skeleton data asset
    /// </summary>
    /// <param name="skeletonDataAssetKey"></param>
    private void SetSkeletonDataAsset(string skeletonDataAssetKey) {
        if (!_skeletonDataAssets.TryGetValue(skeletonDataAssetKey, out var asset)) return;
    
        _skeletonMecanim.skeletonDataAsset = asset;
        _skeletonMecanim.Initialize(true);
        _animator.Update(0f);
    }
    
    private void InitializedSkeletonDataAsset() {
        _skeleton = _skeletonMecanim.skeleton;
    }
    
    private void Flip(bool flip) {
        if (_skeleton != null) _skeleton.ScaleX = flip ? -1f : 1f;
    }
    
    public Vector2 Running() { return _playerMoveDir * 2; }
    public void SetAnimatorBool(int animator, bool value){ _animator.SetBool(animator, value);}
    public bool GetAnimatorBool(int animator) { return _animator.GetBool(animator); }

    // IEnumerator AttackRoutine() {
    //     _animator.SetBool(IsAttacking, true);
    //     AnimatorStateInfo animatorStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
    //
    //     yield return new WaitForSeconds(animatorStateInfo.length);
    //     _animator.SetBool(IsAttacking, false);
    //
    //     PlayerController.Instance.SetStateIdle();
    // }
    //


}

