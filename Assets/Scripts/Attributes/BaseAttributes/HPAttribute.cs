using System;

namespace Assets.Scripts.Attributes.BaseAttributes {
    public class HPAttribute {
        ///Health - HP (Hit Points)  MaxHealth - MaxHP
        public float HP { get; set; }
        public float MaxHP { get; set; }

        public HPAttribute(float hp, float maxHP) {
            HP = hp;
            MaxHP = maxHP;
        }

        public void TakeDamage(float damage) {
            HP = Math.Max(HP - damage, 0); 
        }

        public bool Heal(float amount) {
            if(HP == MaxHP) return false;
            HP = Math.Min(HP + amount, MaxHP); 
            return true;
        }

        public void SetHPToZero() {
            HP = 0; 
        }
    }
}
