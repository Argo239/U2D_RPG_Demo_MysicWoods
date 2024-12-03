using Assets.Scripts.Stats.StatsOperation;
using UnityEngine;

namespace Assets.Scripts.Stats.BaseStats {
    public class DefenseStat {
        public StatValue defense { get; protected set; }
        public StatValue damageDecrease { get; protected set; }
        public IStatValueFactory StatValueFactory;

        private const float DefenseCoefficient = 20f;

        public DefenseStat(float defense, float damageDecrease, StatsMediator statsMediator) {
            StatValueFactory = new StatValueFactory(statsMediator);
            this.defense = StatValueFactory.Create(nameof(this.defense), defense);
            this.damageDecrease = StatValueFactory.Create(nameof(this.damageDecrease), damageDecrease);
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
