using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimationReferenceAsset : MonoBehaviour {

    public static SetAnimationReferenceAsset Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    [SerializeField] public AnimationReferenceAsset idle, walk, run, attack;

}

