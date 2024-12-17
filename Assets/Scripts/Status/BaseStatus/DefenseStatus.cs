using Assets.Scripts.Stats.StatsOperation;
using UnityEngine;

namespace Assets.Scripts.Stats.BaseStatus {
    public class DefenseStatus {
        public StatusValue defense { get; protected set; }
        public StatusValue damageDecrease { get; protected set; }
        public IStatusValueFactory StatusValueFactory;

        private const float DefenseCoefficient = 20f;

        public DefenseStatus(float defense, float damageDecrease, StatusMediator statusMediator) {
            StatusValueFactory = new StatusValueFactory(statusMediator);
            this.defense = StatusValueFactory.Create(nameof(this.defense), defense);
            this.damageDecrease = StatusValueFactory.Create(nameof(this.damageDecrease), damageDecrease);
        }

        /// <summary>   
        /// Return the value after defense
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public float CalculateDamageAfterDefense(float amount) {
            float defenseEffectiveness = defense.CurrentValue / (defense.CurrentValue + DefenseCoefficient);
            return Mathf.Max(amount - (amount * defenseEffectiveness), 0);
        }

        /// <summary>
        /// Return the value after amount reduction
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public float CalculateDamageAfterReduction(float amount) {
            return Mathf.Max(amount * (1 - damageDecrease.CurrentValue), amount * 0.9f);
        }

        public float CalculateFinalDamage(float amount) {
            return CalculateDamageAfterReduction(CalculateDamageAfterDefense(amount));
        }
    }
}
