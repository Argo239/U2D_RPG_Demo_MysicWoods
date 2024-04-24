using Spine;
using Spine.Unity;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAnimator : MonoBehaviour {
        
    private enum MoveDirection {
        None,
        Up,
        Down, 
        Left,
        Right
    }

    [SerializeField] private bool consoleMessage = false;

    [Space]
    [Header("SkeletonDataAsset")]
    [SerializeField] private SkeletonDataAsset frontAsset;
    [SerializeField] private SkeletonDataAsset backAsset;
    [SerializeField] private SkeletonDataAsset sideAsset;
    [SerializeField] private SkeletonAnimation skeletonAnimation;

    [Space]
    private Player player; 
    private Spine.AnimationState spineAnimationState;
    private Skeleton skeleton;
    private TrackEntry up, down, left, right;
    private Player.PlayerState currentPlayerState;//当前动画
    private Player.PlayerState modelState;
    
    
    private void Awake() {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
    }

    private void Update() {
        if(skeletonAnimation is null) return;
        if(player is null) return;
        
        if (currentPlayerState != modelState)
        {
            SetAnimationState(modelState, player.GetMoveDir());
            BlendAnimations(player.GetMoveDir());
            currentPlayerState = modelState;
        }

        Utils.LogMessage(consoleMessage,
            $"---MoveDir: {player.GetMoveDir()} ---CurrentState: {modelState} ---CurrentAnimationName: {skeletonAnimation.AnimationName}");
    }

    private void SetAnimationState(Player.PlayerState animationState, Vector3 moveDir) {
        MoveDirection direction = GetMoveDirection(moveDir);
        Spine.Animation nextAnimation;

        switch (animationState) {
            default:
                nextAnimation = SetAnimationStateIdle(direction);
                break;    
            case Player.PlayerState.Idle:
                nextAnimation = SetAnimationStateIdle(direction);
                break;
            case Player.PlayerState.Walk:
                nextAnimation = SetAnimationStateWalk(direction);
                break;
            case Player.PlayerState.Run:
                nextAnimation = SetAnimationStateRun(direction);
                break;
            case Player.PlayerState.Attack:
                nextAnimation = SetAnimationStateIdle(direction);
                break;
        }

        spineAnimationState.SetAnimation(0, nextAnimation, true);
    }

    /// <summary>
    /// Set Animation state
    /// </summary>
    /// <param name="animation">Animation name</param>
    /// <param name="loop"></param>
    /// <param name="timeScale"></param>
    private TrackEntry SetAnimation(int trackIndex, AnimationReferenceAsset animation, bool loop, float timeScale) {
        if (animation == null || animation.name.Equals(currentPlayerState)) return null;
        if (animation == null) return null;
        // currentAnimation = animation.name;
        TrackEntry entry = spineAnimationState.SetAnimation(trackIndex, animation, loop);
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
                SetBackDirection();
                return AnimationReferenceAssetBack.instance.idle;
            case MoveDirection.Down:
                SetFrontDirection();
                return AnimationReferenceAssetFront.instance.idle;
            case MoveDirection.Left:
                SetSideDirection();
                Flip(true);
                return AnimationReferenceAssetSide.instance.idle;
            case MoveDirection.Right:
                SetSideDirection();
                Flip(false); 
                return AnimationReferenceAssetSide.instance.idle;
        }
    }

    private Spine.Animation SetAnimationStateWalk(MoveDirection direction) {
        switch (direction) {
            default: return null;
            case MoveDirection.Up:
                SetBackDirection();
                return AnimationReferenceAssetBack.instance.walk;
            case MoveDirection.Down:
                SetFrontDirection(); 
                return AnimationReferenceAssetFront.instance.walk;
            case MoveDirection.Left:
                SetSideDirection();
                Flip(true); 
                return AnimationReferenceAssetSide.instance.walk;
            case MoveDirection.Right:
                SetSideDirection();
                Flip(false);
                return AnimationReferenceAssetSide.instance.walk;
        }
    }

    private Spine.Animation SetAnimationStateRun(MoveDirection direction) {
        switch (direction) {
            default: return null;   
            case MoveDirection.Up:
                SetBackDirection();
                return AnimationReferenceAssetBack.instance.run;
            case MoveDirection.Down:
                SetFrontDirection();
                return AnimationReferenceAssetFront.instance.run;
            case MoveDirection.Left:
                SetSideDirection();
                Flip(true);
                return AnimationReferenceAssetSide.instance.run;
            case MoveDirection.Right:
                SetSideDirection();
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
        currentPlayerState = ;
    }

    private void SetFrontDirection() {
        ChangeSkeletonData(frontAsset);
    }

    private void SetBackDirection() {
        ChangeSkeletonData(backAsset);
    }

    private void SetSideDirection() {
        ChangeSkeletonData(sideAsset);
    }

    private void Flip(bool flip) {
        skeleton.ScaleX = flip ? -1f : 1f;
    }
}

