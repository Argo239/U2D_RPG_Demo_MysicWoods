using System.Collections.Generic;

namespace Assets.Scripts.Stats.StatsOperation {
    public class StatusType {
        public string Name { get; set; }
        public StatusType(string name) => Name = name;
        public override string ToString() => Name;
    }

    public static class StatTypeRegistry {
        private static readonly Dictionary<string, StatusType> statusTypes = new();

        public static StatusType RegisterStatType(string name) {
            if (!statusTypes.ContainsKey(name)) {
                var statType = new StatusType(name);
                statusTypes[name] = statType;
                return statType;
            }
            return statusTypes[name];
        }

        public static StatusType GetStatType(string name) { return statusTypes.GetValueOrDefault(name); }

        public static IEnumerable<StatusType> GetStatTypes() { return statusTypes.Values; }
    }
}
