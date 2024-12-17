using Assets.Scripts.Stats.BaseStatus;

public abstract class BaseStatus {
    public HealthStatus HealthStatus { get; protected set; }
    public ManaStatus ManaStatus { get; protected set; }
    public AttackStatus AttackStatus { get; protected set; }
    public DefenseStatus DefenseStatus { get; protected set; }
    public SpeedStatus SpeedStatus { get; protected set; }
    public WeightStatus WeightStatus { get; protected set; }

    protected BaseStatus(HealthStatus healthStatus, ManaStatus manaStatus, AttackStatus attackStatus, DefenseStatus defenseStats, SpeedStatus speedStatus, WeightStatus weightStatus) {
        HealthStatus = healthStatus;
        ManaStatus = manaStatus;
        AttackStatus = attackStatus;
        DefenseStatus = defenseStats;
        SpeedStatus = speedStatus;
        WeightStatus = weightStatus;
    }
}