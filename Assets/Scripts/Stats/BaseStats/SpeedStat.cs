using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Stats.BaseStats {
    public class SpeedStat  {
        public StatValue Speed { get; protected set; }

        public SpeedStat(float speed) {
            Speed = new StatValue("SpeedStat", speed);
        }
    }
}
