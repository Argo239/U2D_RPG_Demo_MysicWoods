using System.Collections.Generic;

namespace Assets.Scripts.Stats.StatsOperation {
    public class StatType {
        public string Name { get; set; }
        public StatType(string name) => Name = name;
        public override string ToString() => Name;
    }

    public static class StatTypeRegistry {
        private static readonly Dictionary<string, StatType> statTypes = new();

        public static StatType RegisterStatType(string name) {
            if (!statTypes.ContainsKey(name)) {
                var statType = new StatType(name);
                statTypes[name] = statType;
                return statType;
            }
            return statTypes[name];
        }

        public static StatType GetStatType(string name) { return statTypes.GetValueOrDefault(name); }

        public static IEnumerable<StatType> GetStatTypes() { return statTypes.Values; }
    }
}
