using System;

namespace Assets.Scripts.Attributes.BaseAttributes {
    public class MPAttribute {
        // Mana - MP (Mana Points)  MaxMana - MaxMP
        public float MP { get; set; }
        public float MaxMP { get; set; }

        public MPAttribute(float mp, float maxMP) {
            MP = mp;
            MaxMP = maxMP;
        }

        public bool UseMP(float amount) { 
            if(amount > MP) return false;
            MP = Math.Max(MP - amount, 0);
            return true;
        }

        public bool ReplyMP(float amount) {
            if(MaxMP == MP) return false;
            MP = Math.Max(MP + amount, MaxMP);
            return true;
        }
    }
}
