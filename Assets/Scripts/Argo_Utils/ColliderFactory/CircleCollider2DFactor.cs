using System;
using UnityEngine;

public class CircleCollider2DFactor : BaseCollider {
    private CalculateHitboxRange _calHitRange = new CalculateHitboxRange();

    public override Collider2D CreateCollider(GameObject gameObject, HitboxConfig config, Vector2 direction) {
        var circleConfig = config as CircleHitboxRangeConfig
                ?? throw new ArgumentException("Expect CircleHitboxRangeConfig");

        var cirCol = gameObject.AddComponent<CircleCollider2D>();
        cirCol.isTrigger = true;
        cirCol.radius = circleConfig.radius;
        
        var hitbox = gameObject.AddComponent<Hitbox>();
        hitbox.lifetime = circleConfig.Lifetime;

        return cirCol;
    }
}
