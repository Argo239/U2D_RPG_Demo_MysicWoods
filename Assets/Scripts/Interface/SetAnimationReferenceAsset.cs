using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimationReferenceAsset : MonoBehaviour {

    public static SetAnimationReferenceAsset Instance { get; private set; }

    [SerializeField] public AnimationReferenceAsset idleAnimation;

}

