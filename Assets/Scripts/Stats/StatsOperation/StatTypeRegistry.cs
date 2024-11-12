using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Stats.StatsOperation {
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
