using UnityEngine;

namespace Assets.Scripts.Attributes.BaseAttributes {
    public class HealthAttribute {
        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }

        public HealthAttribute(float maxHealth, float currentHealth) {
            MaxHealth = maxHealth;
            CurrentHealth = currentHealth;
        }

        public void TakeDamage(float damage) {
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, MaxHealth);
        }

        public void Heal(float amount) {
            CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0,  MaxHealth);
        }

        public void SetHealthToMax() => CurrentHealth = MaxHealth;
        public void SetHealthToZero() => CurrentHealth = 0;

        public float GetCurrentHealth() => CurrentHealth;
        public float GetMaxHealth() => MaxHealth;
    }
}
