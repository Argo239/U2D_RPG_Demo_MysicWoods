using Assets.Scripts.Stats.BaseStats;
using Assets.Scripts.Stats.StatsOperation;

public interface IStatValueFactory {
    StatValue Create(string name, float baseValue);
}

public class StatValueFactory : IStatValueFactory {
    private readonly StatsMediator statsMediator;

    public StatValueFactory(StatsMediator statsMediator) {
        this.statsMediator = statsMediator;
    }

    public StatValue Create(string value, float baseValue) {
        return new StatValue(value, baseValue, statsMediator);
    }
}