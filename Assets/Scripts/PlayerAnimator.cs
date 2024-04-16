using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.Diagnostics;

public class PlayerAnimator : MonoBehaviour {

    [SerializeField] private bool consoleMessage = false;

    [Space]
    [SerializeField] private Player player;
    [SerializeField] private string currentAnimation;

    Animator animator;
    Spine.AnimationState spineAnimationState;
    Spine.Skeleton skeleton;

    [Header("SpineAnimation")]
    [SerializeField] SkeletonAnimation skeletonAnimation;
    [SerializeField] AnimationReferenceAsset idelFront;
    [SerializeField] AnimationReferenceAsset runFront;


    private void Awake() {
        animator = GetComponent<Animator>();
        skeletonAnimation = GetComponent<SkeletonAnimation>();

        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
    }


    private void Start() {

    }

    private void Update() {
        Utils.LogMessage(consoleMessage, player.GetMoveDir());
        SetCharacterState(player.GetMoveDir());
    }

    private void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale) {
        if (animation.name.Equals(currentAnimation)) return;
        spineAnimationState.SetAnimation(0, animation, loop).TimeScale = timeScale;
        currentAnimation = animation.name;
    }

    private void SetCharacterState(Vector3 moveDir) {
        if(moveDir == Vector3.zero) {
            SetAnimation(idelFront, true, 1f);
        }else if(moveDir == Vector3.left) {
            SetAnimation(runFront, true, 1f);
            transform.localScale = new Vector3(2f, 2f);
        }else if(moveDir == Vector3.right) {
            SetAnimation(runFront, true, 1f);
            transform.localScale = new Vector3(-2f, 2f);
        }
    }

}
