using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Attributes.BaseAttributes {
    public class DefenseAttribute {

        public float BaseDefense { get; private set; }
        public float DamageReductionPercent { get; private set; }

        public DefenseAttribute(float baseDefense, float damageReductionPercent) {
            BaseDefense = baseDefense;
            DamageReductionPercent = damageReductionPercent;
        }

        /// <summary>
        /// Return the value after defense
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public float CalculateDamageAfterDefense(float damage) {
            return Mathf.Max(damage - BaseDefense, 1);
        }

        /// <summary>
        /// Return the value after damage reduction
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public float CalculateDamageAfterReduction(float damage) {
            return Mathf.Max(damage * (1 - DamageReductionPercent), 1);
        }

        public float CalculateFinalDamage(float damage) {
            return CalculateDamageAfterReduction(CalculateDamageAfterDefense(damage));
        }

        public float GetDefense() => BaseDefense;
        public float GetDamageReduction() => DamageReductionPercent;
    }
}
