using System;
using UnityEngine;

public class SectorCollider2DFactory : BaseCollider {
    private readonly CalculateHitboxRange _calHitRange = new CalculateHitboxRange();

    public override Collider2D CreateCollider(GameObject gameObject, HitboxConfig config, Vector2 spawnDir) {
        var sectorConfig = config as SectorHitboxRangeConfig
            ?? throw new ArgumentException("Expect SectorHitboxRangeConfig");

        var polyCol = gameObject.AddComponent<PolygonCollider2D>();
        polyCol.isTrigger = true;
        polyCol.offset = sectorConfig.Offset;
        polyCol.points = _calHitRange.GenerateSector(sectorConfig, spawnDir);

        var hitbox = gameObject.AddComponent<Hitbox>();
        hitbox.lifetime = sectorConfig.Lifetime;

        return polyCol;
    }
}
