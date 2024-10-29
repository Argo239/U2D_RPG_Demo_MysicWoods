using UnityEngine;
using UnityEngineInternal;

namespace Assets.Scripts.Attributes.BaseAttributes {
    public class AttackAttribute {
        public float BaseAttack { get; private set; }
        public float BuffedAttack { get; private set; }
        public float DamageBuff { get; private set; }
        public float FinalDamage => BaseAttack + BuffedAttack;

        public AttackAttribute(float baseAttack, float damageBuff = 1.0f) {
            BaseAttack = baseAttack;
            BuffedAttack = 0;
            DamageBuff = damageBuff;
        }

        public void ApplyDamageBuff() {
            BuffedAttack = BaseAttack * (DamageBuff - 1); 
        }

        public void RemoveDamageBuff() {
            BuffedAttack = 0;
        }

        public float GetBaseAttack() => BaseAttack;
        public float GetDamageBuff() => DamageBuff;
        public float GetFinalDamage() => FinalDamage;
    }
}
