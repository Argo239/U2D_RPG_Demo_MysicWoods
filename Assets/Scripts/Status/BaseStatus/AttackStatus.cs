using Assets.Scripts.Stats.StatsOperation;
using UnityEditor.Experimental;
using UnityEngine;

namespace Assets.Scripts.Stats.BaseStatus {
    public class AttackStatus {
        public StatusValue attack { get; }
        public StatusValue damageIncrease { get; }
        public IStatusValueFactory StatusValueFactory;

        public AttackStatus(float attack, float damageIncrease, StatusMediator statusMediator) {
            StatusValueFactory = new StatusValueFactory(statusMediator);
            this.attack = StatusValueFactory.Create(nameof(this.attack), attack);
            this.damageIncrease = StatusValueFactory.Create(nameof(this.damageIncrease), damageIncrease);
        }

        public float CalculateDamageAfterDamageIncrease() {
            return attack.CurrentValue * (1 + damageIncrease.CurrentValue);
        }
    }
}
