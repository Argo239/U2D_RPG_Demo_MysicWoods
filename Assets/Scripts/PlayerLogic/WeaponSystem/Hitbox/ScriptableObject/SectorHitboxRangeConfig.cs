using UnityEngine;

[CreateAssetMenu(fileName = "SectorHitboxRange", menuName = "Hitbox Settings/Sector Hitbox Range Data")]
public class SectorHitboxRangeConfig : HitboxConfig {
    [Header("Outer Radius / 外半徑")]
    public float OuterRadius;

    [Header("Inner Radius / 内半徑")]
    public float InnerRadius;

    [Header("Sweep Angle in Degrees / 扇形角度")]
    public float Angle;

    [Header("Number of Points / 點數量")]
    public int PointCount;

    [Header("Collider Offset / 碰撞體偏移")]
    public Vector2 Offset;
}
