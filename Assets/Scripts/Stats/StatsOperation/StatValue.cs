using Assets.Scripts.Stats.StatsOperation;

namespace Assets.Scripts.Stats.BaseStats {
    public class StatValue {
        public StatType StatType { get; }
        public float BaseValue { get; set; }
        public float CurrentValue { get; set; }

        public StatValue(string type , float baseValue) {
            StatType = StatTypeRegistry.RegisterStatType(type);
            BaseValue = baseValue;
            CurrentValue = baseValue;
        }

        public void ApplyPerManentGain(float amount) {
            BaseValue += amount;
            CurrentValue = BaseValue;
        }

        public float GetCurrentValue(StatsMediator mediator) {
            var query = new Query(StatType, BaseValue);
            mediator.PerformQuery(query);
            CurrentValue = query.Value;
            return CurrentValue;
        }

        public override string ToString() => $"{StatType}: {BaseValue}";
    }
}
