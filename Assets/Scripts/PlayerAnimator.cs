using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEditor.PackageManager.UI;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.EventSystems;

public class PlayerAnimator : MonoBehaviour {
        
    private enum MoveDirection {
        None,
        Up,
        Down, 
        Left,
        Right
    }
    private enum State {
        Idle,
        Walk,
        Run,
        Attack
    }

    [SerializeField] private bool consoleMessage = false;

    [Space]
    [SerializeField] private SkeletonDataAsset frontAsset;
    [SerializeField] private SkeletonDataAsset backAsset;
    [SerializeField] private SkeletonDataAsset sideAsset;
    [SerializeField] private SkeletonAnimation skeletonAnimation;

    [Space]
    private Spine.AnimationState spineAnimationState;
    private Spine.Skeleton skeleton;
    private TrackEntry up, down, left, right;

    [SerializeField] private Player player;
    [SerializeField] private string currentAnimation; //µ±Ç°¶¯»­
    [SerializeField] private State state;



    private void Awake() {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
    }

    private void Update() {
        var currentModelState = state;

        if()




        SetCharacterState();
        SetAnimtionState(state, player.GetMoveDir());
        BlendAnimations(player.GetMoveDir());

        Utils.LogMessage(consoleMessage, $"{player.GetMoveDir()} _ {state} _ {skeletonAnimation.AnimationName} _ {currentAnimation}");
    }

    private void SetCharacterState() {
        if(player.GetMoveDir() == Vector3.zero) {
            state = State.Idle;
        } else {
            state = State.Walk;
        }
    }

    private void SetAnimtionState(State state, Vector3 moveDir) {
        MoveDirection direction = GetMoveDirection(moveDir);
        Spine.Animation nextAnimation;

        switch (state) {
            default:
                nextAnimation = SetAnimationStateIdle(direction); break;    
            case State.Idle:
                nextAnimation = SetAnimationStateIdle(direction);
                break;
            case State.Walk:
                nextAnimation = SetAnimationStateWalk(direction);
                break;
            case State.Run:
                nextAnimation = SetAnimationStateRun(direction);
                break;
            case State.Attack:
                nextAnimation = SetAnimationStateIdle(direction);
                break;
        }

        spineAnimationState.SetAnimation(0, nextAnimation, true);
    }

    /// <summary>
    /// Set Animation state
    /// </summary>
    /// <param name="animation">Animtion name</param>
    /// <param name="loop"></param>
    /// <param name="timeScale"></param>
    private TrackEntry SetAnimation(int trackIntex, AnimationReferenceAsset animation, bool loop, float timeScale) {
        if (animation == null || animation.name.Equals(currentAnimation)) return null;
        if (animation == null) return null;
        currentAnimation = animation.name;
        TrackEntry entry = spineAnimationState.SetAnimation(trackIntex, animation, loop);
        entry.TimeScale = timeScale;
        return entry;
    }

    /// <summary>
    /// Get Move Direction
    /// </summary>
    /// <param name="moveDir"></param>
    /// <returns></returns>
    private MoveDirection GetMoveDirection(Vector3 moveDir) {
        if (moveDir == Vector3.zero) return MoveDirection.None;
        return Mathf.Abs(moveDir.x) > Mathf.Abs(moveDir.y) ? moveDir.x > 0 ? MoveDirection.Right : MoveDirection.Left : moveDir.y > 0 ? MoveDirection.Up : MoveDirection.Down;
    }

    private void BlendAnimations(Vector3 moveDir) {
        if (up == null || down == null || left == null || right == null) return;

        const float blendSpeed = 5f;

        up.Alpha = Mathf.Lerp(up.Alpha, (moveDir.y > 0f) ? moveDir.y : 0f, Time.deltaTime * blendSpeed);
        down.Alpha = Mathf.Lerp(down.Alpha, (moveDir.y < 0f) ? Mathf.Abs(moveDir.y) : 0f, Time.deltaTime * blendSpeed);
        left.Alpha = Mathf.Lerp(left.Alpha, (moveDir.x < 0f) ? Mathf.Abs(moveDir.x) : 0f, Time.deltaTime * blendSpeed);
        right.Alpha = Mathf.Lerp(right.Alpha, (moveDir.x > 0f) ? moveDir.x : 0f, Time.deltaTime * blendSpeed);

        skeleton.SetBonesToSetupPose();

    }

    private Spine.Animation SetAnimationStateIdle(MoveDirection direction) {
        switch (direction) {
            default: return null;
            case MoveDirection.Up:
                SetBackDirction();
                return AnimationReferenceAssetBack.instance.idle;
            case MoveDirection.Down:
                SetFrontDirction();
                return AnimationReferenceAssetFront.instance.idle;
            case MoveDirection.Left:
                SetSideDirction();
                Flip(true);
                return AnimationReferenceAssetSide.instance.idle;
            case MoveDirection.Right:
                SetSideDirction();
                Flip(false); 
                return AnimationReferenceAssetSide.instance.idle;
        }
    }

    private Spine.Animation SetAnimationStateWalk(MoveDirection direction) {
        switch (direction) {
            default: return null;
            case MoveDirection.Up:
                SetBackDirction();
                return AnimationReferenceAssetBack.instance.walk;
            case MoveDirection.Down:
                SetFrontDirction(); 
                return AnimationReferenceAssetFront.instance.walk;
            case MoveDirection.Left:
                SetSideDirction();
                Flip(true); 
                return AnimationReferenceAssetSide.instance.walk;
            case MoveDirection.Right:
                SetSideDirction();
                Flip(false);
                return AnimationReferenceAssetSide.instance.walk;
        }
    }

    private Spine.Animation SetAnimationStateRun(MoveDirection direction) {
        switch (direction) {
            default: return null;   
            case MoveDirection.Up:
                SetBackDirction();
                return AnimationReferenceAssetBack.instance.run;
            case MoveDirection.Down:
                SetFrontDirction();
                return AnimationReferenceAssetFront.instance.run;
            case MoveDirection.Left:
                SetSideDirction();
                Flip(true);
                return AnimationReferenceAssetSide.instance.run;
            case MoveDirection.Right:
                SetSideDirction();
                Flip(false);
                return AnimationReferenceAssetSide.instance.run;
        }
    }

    /// <summary>
    /// Change the Skeleton Data Asset
    /// </summary>
    /// <param name="newAsset"></param>
    private void ChangeSkeletonData(SkeletonDataAsset newAsset) {
        skeletonAnimation.skeletonDataAsset = newAsset;
        skeletonAnimation.Initialize(skeletonAnimation);
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
        currentAnimation = "";
    }

    public void SetFrontDirction() {
        ChangeSkeletonData(frontAsset);
    }

    public void SetBackDirction() {
        ChangeSkeletonData(backAsset);
    }

    public void SetSideDirction() {
        ChangeSkeletonData(sideAsset);
    }

    public void Flip(bool flip) {
        skeleton.ScaleX = flip ? -1f : 1f;
    }
}

