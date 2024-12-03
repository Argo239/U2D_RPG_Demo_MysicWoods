using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine.Playables;
using UnityEngine.Video;

namespace Assets.Scripts.Stats.StatsOperation {
    public class StatsMediator {
        readonly List<StatModifier> listModifiers = new();
        readonly Dictionary<StatType, IEnumerable<StatModifier>> modifiersCache = new();
        readonly Dictionary<StatType, float> lastQueryResults = new();
        readonly object lockObj = new();
        readonly IStatModifierApplicationOrder applicationOrder = new NormalStatModifierOrder();

        public event Action<StatModifier> OnModifierRemoved;

        public void Update(float deltaTime) {
            foreach (var modifier in listModifiers.ToList()) {
                modifier.Update(deltaTime);
                if (modifier.MarkedForRemoval) {
                    modifier.Dispose();
                }
            }
        }

        public void AddModifier(StatModifier modifier) {
            lock (lockObj) {
                listModifiers.Add(modifier);
                InvalidateCache(modifier.StatType);
                modifier.MarkedForRemoval = false;

                modifier.OnDispose += _ => InvalidateCache(modifier.StatType);
                modifier.OnDispose += _ => listModifiers.Remove(modifier);
                modifier.OnDispose += RemoveModifier;
            }
        }

        public void RemoveModifier(StatModifier modifier) {
            lock (lockObj) {
                listModifiers.Remove(modifier);
                InvalidateCache(modifier.StatType);
                OnModifierRemoved?.Invoke(modifier);
                UpdateCache(modifier.StatType);
            }
        }

        private void UpdateCache(StatType statType) {
            modifiersCache[statType] = listModifiers
                .Where(mod => mod.StatType == statType)
                .ToList();
            // 更新缓存后，重新计算并更新最后值
            if (lastQueryResults.ContainsKey(statType)) {
                var baseValue = lastQueryResults[statType]; // 获取之前的基础值
                lastQueryResults[statType] = applicationOrder.Apply(modifiersCache[statType], baseValue);
            }
        }

        public void PerformQuery(object sender, Query query) {
            if (!modifiersCache.ContainsKey(query.StatType)) {
                modifiersCache[query.StatType] = listModifiers
                    .Where(mod => mod.StatType == query.StatType)
                    .ToList();
            }
            query.Value = applicationOrder.Apply(modifiersCache[query.StatType], query.BaseValue);
            lastQueryResults[query.StatType] = query.Value;
        }

        public float GetLastQueryResult(StatType statType) {
            return lastQueryResults.TryGetValue(statType, out var value) ? value : 0f;
        }

        public void InvalidateCache(StatType statType) => modifiersCache.Remove(statType);
    }

    public class Query {
        public readonly StatType StatType;
        public readonly float BaseValue;
        public float Value;

        public Query(StatType statType, float baseValue) {
            StatType = statType;
            BaseValue = baseValue;
            Value = baseValue;
        }
    }
}
