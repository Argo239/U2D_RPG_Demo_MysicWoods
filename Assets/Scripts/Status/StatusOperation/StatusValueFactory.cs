using Assets.Scripts.Stats.BaseStatus;
using Assets.Scripts.Stats.StatsOperation;

public interface IStatusValueFactory {
    StatusValue Create(string name, float baseValue);
}

public class StatusValueFactory : IStatusValueFactory {
    private readonly StatusMediator statusMediator;

    public StatusValueFactory(StatusMediator statusMediator) {
        this.statusMediator = statusMediator;
    }

    public StatusValue Create(string value, float baseValue) {
        return new StatusValue(value, baseValue, statusMediator);
    }
}