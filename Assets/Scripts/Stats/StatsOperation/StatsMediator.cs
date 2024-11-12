using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Stats.StatsOperation {
    public class StatsMediator {
        private readonly List<StatModifier> listModifiers = new();
        private readonly Dictionary<StatType, IEnumerable<StatModifier>> modifiersCache = new();

        public void PerformQuery(Query query) { 
            if(!modifiersCache.ContainsKey(query.StatType)) {
                modifiersCache[query.StatType] = listModifiers.Where(mod => mod.StatType == query.StatType).ToList();
            }

            float flatIncrease = modifiersCache[query.StatType]
                .Where(mod => mod.ModifierType == ModifierType.Flat)
                .Sum(mod => mod.Value);

            float PercentageIncrease = modifiersCache[query.StatType]
                .Where(mod => mod.ModifierType == ModifierType.Percentage)
                .Sum(mod => mod.Value);

            query.Value = (query.BaseValue + flatIncrease) * (1 + PercentageIncrease);
        }

        public void AddModifier(StatModifier modifier) {
            listModifiers.Add(modifier);
            InvalidateCache(modifier.StatType);
            modifier.OnDispose += _ => InvalidateCache(modifier.StatType);
            modifier.OnDispose += _ => listModifiers.Remove(modifier);
        }

        public void RemoveModifier(StatModifier modifier) {
            listModifiers.Remove(modifier);
            InvalidateCache(modifier.StatType);
        }

        public void InvalidateCache(StatType statType) => modifiersCache.Remove(statType);
    }
}
