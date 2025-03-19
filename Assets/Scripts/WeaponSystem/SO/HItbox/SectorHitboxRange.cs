using UnityEngine;

[CreateAssetMenu(fileName = "SectorHitboxRange", menuName = "HitboxConfig/SectorHitboxRange")]
public class SectorHitboxRangeConfig : HitboxConfig {
    public string animatorName;
    public float outerRadius;
    public float innerRadius;
    public float angle;
    public int pointCount;
    public Vector2 offset;
}
