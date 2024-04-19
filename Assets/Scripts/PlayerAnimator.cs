using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
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


    [SerializeField] private bool consoleMessage = false;


    [Space]

    Animator animator;
    Spine.AnimationState spineAnimationState;
    Spine.Skeleton skeleton;

    [SerializeField] private Player player;
    //µ±Ç°¶¯»­
    [SerializeField] private string currentAnimation;

    [Header("SpineAnimation")]
    [SerializeField] SkeletonAnimation skeletonAnimation;

    private void Awake() {
        animator = GetComponent<Animator>();
        skeletonAnimation = GetComponent<SkeletonAnimation>();

        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
    }


    private void Start() {

    }

    private void Update() {

    }

    private void SetCharacterState(string state, Vector3 moveDir) {
        MoveDirection direction = GetMoveDirection(moveDir);
        switch (state) {
            default:
                break;
            case "idel":
                //switch (moveDir) {
                //    default :
                //        break;
                //    case 
                //}

                break;
            case "walk":
                break;
            case "run":
                break;
        }
    }

    /// <summary>
    /// Set Animation state
    /// </summary>
    /// <param name="animation">Animtion name</param>
    /// <param name="loop"></param>
    /// <param name="timeScale"></param>
    private void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale) {
        if (animation.name.Equals(currentAnimation)) return;
        spineAnimationState.SetAnimation(0, animation, loop).TimeScale = timeScale;
        currentAnimation = animation.name;
    }

    /// <summary>
    /// Get Move Direction
    /// </summary>
    /// <param name="moveDir"></param>
    /// <returns></returns>
    private MoveDirection GetMoveDirection(Vector3 moveDir) {
        if (moveDir == Vector3.zero) return MoveDirection.None;
        else if (Mathf.Abs(moveDir.x) > Mathf.Abs(moveDir.y)) return moveDir.x > 0 ? MoveDirection.Right : MoveDirection.Left;
        else return moveDir.y > 0 ? MoveDirection.Up : MoveDirection.Down;
    }

    private void SetAnimationStateName(string animation, string moveDir) {
        if(string.IsNullOrEmpty(animation) || string.IsNullOrEmpty(moveDir)) return;



        //SetAnimation(, true, 1f);
    }

    //SetAnimation(walkFront, true, 1f);

}

