using UnityEngine;

[CreateAssetMenu(fileName = "ComboStepData", menuName = "Combo Settings/Combo Step Data")]
public class ComboStepData : ScriptableObject {
    [Header("Animation Name / 動畫名")]
    public string AnimationName = "Default";

    [Header("Combo Segment / 攻擊段數")]
    public int ComboSegment = 0;

    [Header("Damage Multiplier / 傷害倍率")]
    public float DamageMultiplier = 1f;
    
    [Header("Stiff Duration (s) / 硬直時間")]
    public float StiffDuration = .5f;

    [Header("Window (0-1) / 下一段輸入窗口")]
    [Range(0f, 1f)] public float WindowStart = .2f;
    [Range(0f, 1f)] public float WindowEnd = .7f;

    [Header("Hitbox Settings / 碰撞體設置")]
    [Header("Collider Size / 碰撞體尺寸")]
    public Vector2 ColliderSize = new Vector2(1f, 1f);

    [Header("Collider Offset / 碰撞體偏移")]
    public Vector2 ColliderOffset = new Vector2(1f, 1f);

    [Header("Collider Duration (s) / 碰撞體持續時間")]
    public float ColliderDuration = .2f;

    [Header("Hitbox Config / 碰撞體配置")]
    public HitboxConfig HitboxConfig;
}
