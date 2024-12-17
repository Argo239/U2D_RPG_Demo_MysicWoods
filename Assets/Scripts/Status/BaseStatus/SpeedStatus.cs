using Assets.Scripts.Stats.StatsOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Stats.BaseStatus {
    public class SpeedStatus  {
        public StatusValue speed { get; protected set; }
        public IStatusValueFactory StatusValueFactory;

        public SpeedStatus(float speed, StatusMediator statusMediator) {
            StatusValueFactory = new StatusValueFactory(statusMediator);
            this.speed = StatusValueFactory.Create(nameof(this.speed), speed);
        }
    }
}
