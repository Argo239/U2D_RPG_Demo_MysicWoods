using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationReferenceAssetBack : SetAnimationReferenceAsset {

    public static AnimationReferenceAssetBack instance { get; private set; }

    private void Awake() {
        instance = this;
    }
}
