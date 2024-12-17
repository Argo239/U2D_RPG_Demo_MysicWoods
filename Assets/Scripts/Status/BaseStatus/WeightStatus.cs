using Assets.Scripts.Stats.StatsOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Stats.BaseStatus {
    public class WeightStatus {
        public StatusValue weight { get; protected set; }
        public StatusValueFactory StatusValueFactory;

        public WeightStatus(int weight, StatusMediator statusMediator) {
            StatusValueFactory = new StatusValueFactory(statusMediator);
            this.weight = StatusValueFactory.Create(nameof(this.weight), weight);
        }
    }
}
