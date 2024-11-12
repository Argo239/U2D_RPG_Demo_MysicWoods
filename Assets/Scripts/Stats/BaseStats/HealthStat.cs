using UnityEngine;

namespace Assets.Scripts.Stats.BaseStats {
    public class HealthStat {
        public StatValue Health { get; protected set; }
        public StatValue CurrentHealth { get; protected set; }

        public HealthStat(float health, float currentHealth) {
            Health = new StatValue("HealthStat", health);
            CurrentHealth = new StatValue("CurrentHealth", currentHealth);
        }

        /// <summary>
        /// Reduces the current health by a specified damage amount.
        /// Ensures the health does not drop below 0.
        /// </summary>
        /// <param name="amount">The amount of damage taken.</param>
        public void TakeDamage(float amount) {
            CurrentHealth.CurrentValue = Mathf.Clamp(CurrentHealth.CurrentValue - amount, 0, Health.CurrentValue);
        }

        /// <summary>
        /// Increases the current health by a specified amount.
        /// Ensures the health does not exceed the maximum health.
        /// </summary>
        /// <param name="amount">The amount of health to restore.</param>
        public void Heal(float amount) {
            CurrentHealth.CurrentValue = Mathf.Clamp(CurrentHealth.CurrentValue + amount, 0, Health.CurrentValue);
        }

        /// <summary>
        /// Sets the current health to 0, effectively "killing" the character.
        /// </summary>
        public void Kill() {
            CurrentHealth.CurrentValue = 0;
        }

        /// <summary>
        /// Fully restores health to the maximum value.
        /// </summary>
        public void FullHeal() {
            CurrentHealth.CurrentValue = Health.CurrentValue;
        }
    }
}
