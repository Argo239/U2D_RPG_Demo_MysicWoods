namespace Assets.Scripts.Stats.StatsOperation {
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
