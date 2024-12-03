using Assets.Scripts.Stats.StatsOperation;
using System;
public interface IStatModifierFactory {
	StatModifier Create(OperatorType operatorType, StatType statType, float amount, float duration);
}

public class StatModifierFactory : IStatModifierFactory {
    public StatModifier Create(OperatorType operatorType, StatType statType, float amount, float duration) {
        IOperationStrategy strategy = operatorType switch {
            OperatorType.Add => new AddOperation(amount),
            OperatorType.Multiply => new MultiplyOperation(amount),
            _ => throw new ArgumentException()
        };
        return new StatModifier(statType, strategy, duration);
    }
}