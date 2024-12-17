using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine.Playables;
using UnityEngine.Video;

namespace Assets.Scripts.Stats.StatsOperation {
    public class StatusMediator {
        readonly List<StatusModifier> listModifiers = new();
        readonly Dictionary<StatusType, IEnumerable<StatusModifier>> modifiersCache = new();
        readonly Dictionary<StatusType, float> lastQueryResults = new();
        readonly object lockObj = new();
        readonly IStatModifierApplicationOrder applicationOrder = new NormalStatModifierOrder();

        public event Action<StatusModifier> OnModifierRemoved;

        public void Update(float deltaTime) {
            foreach (var modifier in listModifiers.ToList()) {
                modifier.Update(deltaTime);
                if (modifier.MarkedForRemoval) {
                    modifier.Dispose();
                }
            }
        }

        public void AddModifier(StatusModifier modifier) {
            lock (lockObj) {
                listModifiers.Add(modifier);
                InvalidateCache(modifier.StatusType);
                modifier.MarkedForRemoval = false;

                modifier.OnDispose += _ => InvalidateCache(modifier.StatusType);
                modifier.OnDispose += _ => listModifiers.Remove(modifier);
                modifier.OnDispose += RemoveModifier;
            }
        }

        public void RemoveModifier(StatusModifier modifier) {
            lock (lockObj) {
                listModifiers.Remove(modifier);
                InvalidateCache(modifier.StatusType);
                OnModifierRemoved?.Invoke(modifier);
                UpdateCache(modifier.StatusType);
            }
        }

        private void UpdateCache(StatusType statType) {
            modifiersCache[statType] = listModifiers
                .Where(mod => mod.StatusType == statType)
                .ToList();
            // 更新缓存后，重新计算并更新最后值
            if (lastQueryResults.ContainsKey(statType)) {
                var baseValue = lastQueryResults[statType]; // 获取之前的基础值
                lastQueryResults[statType] = applicationOrder.Apply(modifiersCache[statType], baseValue);
            }
        }

        public void PerformQuery(object sender, Query query) {
            if (!modifiersCache.ContainsKey(query.StatusType)) {
                modifiersCache[query.StatusType] = listModifiers
                    .Where(mod => mod.StatusType == query.StatusType)
                    .ToList();
            }
            query.Value = applicationOrder.Apply(modifiersCache[query.StatusType], query.BaseValue);
            lastQueryResults[query.StatusType] = query.Value;
        }

        public float GetLastQueryResult(StatusType statType) {
            return lastQueryResults.TryGetValue(statType, out var value) ? value : 0f;
        }

        public void InvalidateCache(StatusType statType) => modifiersCache.Remove(statType);
    }

    public class Query {
        public readonly StatusType StatusType;
        public readonly float BaseValue;
        public float Value;

        public Query(StatusType statusType, float baseValue) {
            StatusType = statusType;
            BaseValue = baseValue;
            Value = baseValue;
        }
    }
}
