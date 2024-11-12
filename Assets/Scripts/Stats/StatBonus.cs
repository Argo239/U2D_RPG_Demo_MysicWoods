using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Stats {
    public class StatBonus {
        public float BonusValue { get; set; }    

        public StatBonus(float bonusValue) {
            this.BonusValue = bonusValue;
        }

    }
}
