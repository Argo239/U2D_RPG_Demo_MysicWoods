using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Stats.BaseStats {
    public class WeightStat {
        public StatValue Weight { get; protected set; }

        public WeightStat(int weight) {
            Weight = new StatValue("WeightStat", weight);
        }
    }
}
