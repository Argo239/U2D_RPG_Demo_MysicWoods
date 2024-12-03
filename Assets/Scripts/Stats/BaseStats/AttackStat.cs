using Assets.Scripts.Stats.StatsOperation;
using UnityEditor.Experimental;
using UnityEngine;

namespace Assets.Scripts.Stats.BaseStats {
    public class AttackStat {
        public StatValue attack { get; }
        public StatValue damageIncrease { get; }
        public IStatValueFactory StatValueFactory;

        public AttackStat(float attack, float damageIncrease, StatsMediator statsMediator) {
            StatValueFactory = new StatValueFactory(statsMediator);
            this.attack = StatValueFactory.Create(nameof(this.attack), attack);
            this.damageIncrease = StatValueFactory.Create(nameof(this.damageIncrease), damageIncrease);
        }

        public float CalculateDamageAfterDamageIncrease() {
            return attack.CurrentValue * (1 + damageIncrease.CurrentValue);
        }
    }
}
