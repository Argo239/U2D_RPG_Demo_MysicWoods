using UnityEngine;

namespace Assets.Scripts.Attributes.BaseAttributes {
    public class ManaAttribute {
        public float MaxMana { get; private set; }
        public float CurrentMana { get; private set; }

        public ManaAttribute(float maxMana, float currentMana) {
            MaxMana = maxMana;
            CurrentMana = currentMana;
        }

        public void UseMana(float amount) { 
            CurrentMana = Mathf.Clamp(CurrentMana - amount, 0, MaxMana);
        }

        public void ReplyMana(float amount) {
            CurrentMana = Mathf.Clamp(CurrentMana + amount, 0, MaxMana);
        }

        public void SetManaToMax() => CurrentMana = MaxMana;
        public void SetManaToZero() => CurrentMana = 0;

        public float GetMaxMana() => MaxMana;
        public float GetCurrentMana() => CurrentMana;
    }
}
