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

        // 处理所有加法修饰器
        foreach (var statModifier in allModifiers.Where(modifier => modifier.Strategy is AddOperation)) {
            result = statModifier.Strategy.Calculate(result);  // 加法修饰器直接作用于 result
        }

        // 处理所有乘法修饰器
        float multiplyValue = 0f;
        foreach (var statModifier in allModifiers.Where(modifier => modifier.Strategy is MultiplyOperation)) {
            multiplyValue += statModifier.Strategy.Calculate(result);
        }

        return result + multiplyValue;
    }
}