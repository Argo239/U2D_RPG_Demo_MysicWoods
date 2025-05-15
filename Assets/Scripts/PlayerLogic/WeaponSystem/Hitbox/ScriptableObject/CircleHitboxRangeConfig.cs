using UnityEngine;

[CreateAssetMenu(fileName = "CircleHitboxRange", menuName = "Hitbox Settings/Circle Hitbox Range Data")]
public class CircleHitboxRangeConfig : HitboxConfig {
    [Header("Radius / 半徑")]
    public float radius;

    [Header("Number of segment / 分段數")]
    public int segmentCount;

    [Header("Collider Offset / 碰撞體偏移")]
    public Vector2 offset;
}