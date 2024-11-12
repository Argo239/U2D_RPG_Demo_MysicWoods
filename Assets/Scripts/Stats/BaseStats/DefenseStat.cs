using Assets.Scripts.Stats.StatsOperation;
using UnityEngine;

namespace Assets.Scripts.Stats.BaseStats {
    public class DefenseStat {
        public StatValue Defense { get; protected set; }
        public StatValue DamageReductionPercent { get; protected set; }

        private const float DefenseCoefficient = 20f;

        public DefenseStat(float defense, float damageReductionPercent) {
            Defense = new StatValue("DefenseStat", defense);
            DamageReductionPercent = new StatValue("DamageReductionPercent", damageReductionPercent);
        }

        /// <summary>   
        /// Return the value after defense
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public float CalculateDamageAfterDefense(float amount) {
            float defenseEffectiveness = Defense.CurrentValue / (Defense.CurrentValue + DefenseCoefficient);
            return Mathf.Max(amount - (amount * defenseEffectiveness), 0);
        }

        /// <summary>
        /// Return the value after amount reduction
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public float CalculateDamageAfterReduction(float amount) {
            return Mathf.Max(amount * (1 - DamageReductionPercent.CurrentValue), amount * 0.9f);
        }

        public float CalculateFinalDamage(float amount) {
            return CalculateDamageAfterReduction(CalculateDamageAfterDefense(amount));
        }
    }
}
