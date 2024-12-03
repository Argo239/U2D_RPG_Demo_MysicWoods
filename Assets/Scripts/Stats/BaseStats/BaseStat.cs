using Assets.Scripts.Stats.BaseStats;

public abstract class BaseStat {
    public HealthStat HealthStat { get; protected set; }
    public ManaStat ManaStat { get; protected set; }
    public AttackStat AttackStat { get; protected set; }
    public DefenseStat DefenseStat { get; protected set; }
    public SpeedStat SpeedStat { get; protected set; }
    public WeightStat WeightStat { get; protected set; }

    protected BaseStat(HealthStat healthStat, ManaStat manaStat, AttackStat attackStat, DefenseStat defenseStat, SpeedStat speedStat, WeightStat weightStat) {
        HealthStat = healthStat;
        ManaStat = manaStat;
        AttackStat = attackStat;
        DefenseStat = defenseStat;
        SpeedStat = speedStat;
        WeightStat = weightStat;
    }
}