using Assets.Scripts.Stats.BaseStatus;
using Assets.Scripts.Stats.Stat;
using Assets.Scripts.Stats.StatsOperation;

public class PlayerAttributes : BaseStatus {
    public string PlayerName { get; private set; }
    public float CurrentHealth { get; private set; }
    public float MaxHealth { get; private set; }
    public float CurrentMana { get; private set; }
    public float MaxMana { get; private set; }
    public float Attack { get; private set; }
    public float DamageIncrease { get; private set; }
    public float Defense { get; private set; }
    public float DamageDecrease { get; private set; }
    public float Speed { get; private set; }
    public int Weight { get; private set; }
    public int Level { get; private set; }
    public int Experience { get; private set; }
    public int ExperienceToUpgrade { get; private set; }

    public LevelStatus LevelStatus { get; protected set; }

    public PlayerAttributes(PlayerAttributesInitData initialData, StatusMediator statsMediator)
        : base(
            new HealthStatus(initialData.CurrentHealth, initialData.MaxHealth, statsMediator),
            new ManaStatus(initialData.CurrentMana, initialData.MaxMana, statsMediator),
            new AttackStatus(initialData.Attack, initialData.DamageIncrease, statsMediator),
            new DefenseStatus(initialData.Defense, initialData.DamageDecrease, statsMediator),
            new SpeedStatus(initialData.Speed, statsMediator),
            new WeightStatus(initialData.Weight, statsMediator)
        ) {
        PlayerName = initialData.PlayerName;
        LevelStatus = new LevelStatus(initialData.Level, initialData.Experience, initialData.baseExperienceUpgrade, statsMediator);
        Update();
    }

    public void Update() {
        CurrentHealth = HealthStatus.currentHealth.CurrentValue;
        MaxHealth = HealthStatus.maxHealth.CurrentValue;
        CurrentMana = ManaStatus.currentMana.CurrentValue;
        MaxMana = ManaStatus.maxMana.CurrentValue;
        Attack = AttackStatus.attack.CurrentValue;
        DamageIncrease = AttackStatus.damageIncrease.CurrentValue;
        Defense = DefenseStatus.defense.CurrentValue;
        DamageDecrease = DefenseStatus.damageDecrease.CurrentValue;
        Speed = SpeedStatus.speed.CurrentValue;
        Weight = (int)WeightStatus.weight.CurrentValue;
        Level = (int)LevelStatus.level.CurrentValue;
        Experience = LevelStatus.experience;
        ExperienceToUpgrade = LevelStatus.experienceToUpgrade;
    }
}
