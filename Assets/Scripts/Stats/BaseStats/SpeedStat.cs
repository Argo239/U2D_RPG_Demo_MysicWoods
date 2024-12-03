using Assets.Scripts.Stats.StatsOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Stats.BaseStats {
    public class SpeedStat  {
        public StatValue speed { get; protected set; }
        public IStatValueFactory StatValueFactory;

        public SpeedStat(float speed, StatsMediator statsMediator) {
            StatValueFactory = new StatValueFactory(statsMediator);
            this.speed = StatValueFactory.Create(nameof(this.speed), speed);
        }
    }
}
