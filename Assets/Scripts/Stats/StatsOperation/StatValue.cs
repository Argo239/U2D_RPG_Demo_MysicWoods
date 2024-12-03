using Assets.Scripts.Stats.StatsOperation;

namespace Assets.Scripts.Stats.BaseStats {
    public class StatValue {
        public StatType StatType { get; }
        public StatsMediator StatsMediator;

        public float BaseValue { get; set; }

        public float adjustmentLayer;

        public float CurrentValue {
            get {
                float modifierValue = GetModifierValue();
                return modifierValue + adjustmentLayer;
            }
            set {
                float delta = value - CurrentValue;
                adjustmentLayer += delta;
            }
        }

        public StatValue(string statType, float baseValue, StatsMediator statsMediator) {
            StatType = StatTypeRegistry.RegisterStatType(statType);
            BaseValue = baseValue;
            StatsMediator = statsMediator;
            adjustmentLayer = 0;

            StatsMediator.OnModifierRemoved += HandleModifierRemoved;
        }

        // 修饰器被移除时的回调
        private void HandleModifierRemoved(StatModifier modifier) {
            // 重新计算当前值，并更新 adjustmentLayer
            float oldValue = StatsMediator.GetLastQueryResult(StatType);
            float newValue = GetModifierValue();
            adjustmentLayer += oldValue - newValue;
            CurrentValue -= (oldValue - newValue);
        }

        public void ResetAdjustmentLayer() {
            adjustmentLayer = 0;
        }

        private float GetModifierValue() {
            var q = new Query(StatType, BaseValue);
            StatsMediator.PerformQuery(this, q);
            return q.Value;
        }

        public override string ToString() => $"{StatType}: {BaseValue}";
    }
}
