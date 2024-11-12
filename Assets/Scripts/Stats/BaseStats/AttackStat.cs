using UnityEditor.Experimental;
using UnityEngine;

namespace Assets.Scripts.Stats.BaseStats {
    public class AttackStat {
        public StatValue Attack { get; }
        public StatValue DamageIncrease { get; }

        public AttackStat(float attack, float damageIncrease) {
            Attack = new StatValue("AttackStat", attack);
            DamageIncrease = new StatValue("DamageIncrease", damageIncrease);
        }

        public float CalculateDamageAfterDamageIncrease() {
            return Attack.CurrentValue * (1 + DamageIncrease.CurrentValue);
        }
    }
}
