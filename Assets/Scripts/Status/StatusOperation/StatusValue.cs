using Assets.Scripts.Stats.StatsOperation;

namespace Assets.Scripts.Stats.BaseStatus {
    public class StatusValue {
        public StatusType StatusType { get; }
        public StatusMediator StatusMediator;

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

        public StatusValue(string statType, float baseValue, StatusMediator statsMediator) {
            StatusType = StatTypeRegistry.RegisterStatType(statType);
            BaseValue = baseValue;
            StatusMediator = statsMediator;
            adjustmentLayer = 0;

            StatusMediator.OnModifierRemoved += HandleModifierRemoved;
        }

        // 修饰器被移除时的回调
        private void HandleModifierRemoved(StatusModifier modifier) {
            // 重新计算当前值，并更新 adjustmentLayer
            float oldValue = StatusMediator.GetLastQueryResult(StatusType);
            float newValue = GetModifierValue();
            adjustmentLayer += oldValue - newValue;
            CurrentValue -= (oldValue - newValue);
        }

        public void ResetAdjustmentLayer() {
            adjustmentLayer = 0;
        }

        private float GetModifierValue() {
            var q = new Query(StatusType, BaseValue);
            StatusMediator.PerformQuery(this, q);
            return q.Value;
        }

        public override string ToString() => $"{StatusType}: {BaseValue}";
    }
}
