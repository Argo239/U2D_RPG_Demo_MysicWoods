using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationReferenceAssetSide : SetAnimationReferenceAsset {

    public static AnimationReferenceAssetSide instance { get; private set; }

    private void Awake() {
        instance = this;
    }
}
