using System;
using System.Collections.Generic;
using System.Data.Common;
using Interface;
using PlayerLogic;
using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.EventSystems;
using static Argo_Utils.Utils;

public class PlayerAnimator : MonoBehaviour {
    public static PlayerAnimator Instance { get; private set; }

    #region Animator attribute
    private const string ISMOVING = "IsMoving";
    private const string HORIZONTAL = "HorizontalMovement";
    private const string VERTICAL = "VerticalMovement";

    public static readonly int IsMoving = Animator.StringToHash(ISMOVING);
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

    //public void TryToSetAnimation(int animatorId, bool value) {
    //    MoveDirection direction = GetMoveDirection(playerFaceDir);
    //    string skeletonDataAssetKey = GetSkeletonDataAssetKey(direction);

    //    if (string.IsNullOrEmpty(skeletonDataAssetKey)) return;

    //    SetSkeletonDataAsset(skeletonDataAssetKey);
    //    InitializedSkeletonDataAsset();
    //    SetAnimatorDirection();

    //    animator.SetBool(animatorId, value);
    //    Flip(direction == MoveDirection.Left);
    //}

    /// <summary>
    ///     <para>Get the Move Direction</para>
    /// </summary>
    /// <param name="moveDir"></param>
    /// <returns></returns>
    //private MoveDirection GetMoveDirection(Vector2 moveDir) {
    //    if (moveDir == Vector2.zero) return MoveDirection.Down;
    //    return Mathf.Abs(moveDir.x) > Mathf.Abs(moveDir.y) ? moveDir.x > 0 ? MoveDirection.Right : MoveDirection.Left :
    //        moveDir.y > 0 ? MoveDirection.Up : MoveDirection.Down;
    //}

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