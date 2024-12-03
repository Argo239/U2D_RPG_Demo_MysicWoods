using Assets.Scripts.Stats.StatsOperation;
using System.Collections.Generic;
using System.Linq;

public interface IStatModifierApplicationOrder {
    float Apply(IEnumerable<StatModifier> statModifiers, float baseValue);
}

public class NormalStatModifierOrder : IStatModifierApplicationOrder {
    public float Apply(IEnumerable<StatModifier> statModifiers, float baseValue) {
        var allModifiers = statModifiers.ToList();
        float result = baseValue;

        // �������мӷ�������
        foreach (var statModifier in allModifiers.Where(modifier => modifier.Strategy is AddOperation)) {
            result = statModifier.Strategy.Calculate(result);  // �ӷ�������ֱ�������� result
        }

        // �������г˷�������
        float multiplyValue = 0f;
        foreach (var statModifier in allModifiers.Where(modifier => modifier.Strategy is MultiplyOperation)) {
            multiplyValue += statModifier.Strategy.Calculate(result);
        }

        return result + multiplyValue;
    }
}