using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class PlayerAnimator : MonoBehaviour {

    [SerializeField] private bool consoleMessage = false;

    [Space]
    [SerializeField] private Player player;
    [SerializeField] private float timer = 5;
    [SerializeField] private float runWalkDuration = 3f;


    Animator animator;
    Spine.AnimationState spineAnimationState;
    Spine.Skeleton skeleton;


    [Header("SpineEvent")]
    [SpineEvent(dataField: "skeletonAnimation", fallbackToTextField: true)]
    public string eventName;

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
        //Utils.LogMessage(consoleMessage, Utils.CountdownTimer(timer));
    }

    private void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale) {
        spineAnimationState.SetAnimation(0, animation, loop).TimeScale = timeScale;
    }


}
