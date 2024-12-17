using PlayerLogic;
using Spine;
using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAnimator : MonoBehaviour {
    public static PlayerAnimator Instance { get; private set; }

    #region Animator attribute
    private const string ISMOVING = "IsMoving";
    private const string ISDEAD = "IsDead";
    private const string HORIZONTAL = "HorizontalMovement";
    private const string VERTICAL = "VerticalMovement";

    public static readonly int IsMoving = Animator.StringToHash(ISMOVING);
    public static readonly int IsDead = Animator.StringToHash(ISDEAD);
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
    private Animator animator;
    private Skeleton skeleton;
    private SkeletonMecanim skeletonMecanim;
    private AnimatorStateMachine stateMachine;
    private PlayerController _playerController;

    private Dictionary<string, SkeletonDataAsset> skeletonDataAssets;
    #endregion


    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        animator = GetComponent<Animator>();
        skeletonMecanim = GetComponent<SkeletonMecanim>();
        skeletonMecanim.Initialize(true, true);

        skeletonDataAssets = new Dictionary<string, SkeletonDataAsset> {
            { frontSkeletonDataAsset.name, frontSkeletonDataAsset },
            { backSkeletonDataAsset.name, backSkeletonDataAsset },
            { sideSkeletonDataAsset.name, sideSkeletonDataAsset }
        };
    }

    private void Start() {
        _playerController = PlayerController.Instance;
    }


    public void TryToSetAnimation(int animatorID, bool value, MoveDirection direction, Vector2 playerLookDir) {
        string skeletonDataAssetKey = GetSkeletonDataAssetKey(direction);

        if (string.IsNullOrEmpty(skeletonDataAssetKey)) return;

        SetSkeletonDataAsset(skeletonDataAssetKey);
        InitializedSkeletonDataAsset();
        SetAnimatorDirection(playerLookDir);

        animator.SetBool(animatorID, value);

        //Flip
        skeleton.ScaleX = direction == MoveDirection.Left ? -1f : 1f;
    }

    public void PlayAnimation(string animationName) {
        if (_playerController == null) return;
        animator.Play(animationName);
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
    private void SetAnimatorDirection(Vector2 playerLookDir) {
        animator.SetFloat(HorizontalMovement, playerLookDir.x);
        animator.SetFloat(VerticalMovement, playerLookDir.y);
    }

    /// <summary>
    /// Change the skeleton data asset
    /// </summary>
    /// <param name="skeletonDataAssetKey"></param>
    private void SetSkeletonDataAsset(string skeletonDataAssetKey) {
        if (!skeletonDataAssets.TryGetValue(skeletonDataAssetKey, out var asset)) return;

        if (skeletonMecanim.skeletonDataAsset != asset) {
            skeletonMecanim.skeletonDataAsset = asset;
            skeletonMecanim.Initialize(true);
            animator.Update(0f);
        }
    }

    private void InitializedSkeletonDataAsset() {
        skeleton = skeletonMecanim.skeleton;
    }

    public AnimatorStateMachine GetStateMachine() => stateMachine;
    public bool GetAnimatorBool(int animator) => this.animator.GetBool(animator);
    public void SetAnimatorBool(int animator, bool value) => this.animator.SetBool(animator, value);
}