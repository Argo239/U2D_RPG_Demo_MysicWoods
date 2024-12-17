using Assets.Scripts.Stats.StatsOperation;
using System;
public interface IStatusModifierFactory {
	StatusModifier Create(OperatorType operatorType, StatusType statusType, float amount, float duration);
}

public class StatusModifierFactory : IStatusModifierFactory {
    public StatusModifier Create(OperatorType operatorType, StatusType statusType, float amount, float duration) {
        IOperationStrategy strategy = operatorType switch {
            OperatorType.Add => new AddOperation(amount),
            OperatorType.Multiply => new MultiplyOperation(amount),
            _ => throw new ArgumentException()
        };
        return new StatusModifier(statusType, strategy, duration);
    }
}