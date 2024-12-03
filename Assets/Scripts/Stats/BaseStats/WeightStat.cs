using Assets.Scripts.Stats.StatsOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Stats.BaseStats {
    public class WeightStat {
        public StatValue weight { get; protected set; }
        public StatValueFactory StatValueFactory;

        public WeightStat(int weight, StatsMediator statsMediator) {
            StatValueFactory = new StatValueFactory(statsMediator);
            this.weight = StatValueFactory.Create(nameof(this.weight), weight);
        }
    }
}
