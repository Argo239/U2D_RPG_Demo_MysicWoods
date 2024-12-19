using Assets.Scripts.Stats.StatsOperation;
using UnityEngine;

namespace Assets.Scripts.Stats.BaseStatus {
    public class HealthStatus {
        public StatusValue currentHealth { get; protected set; }
        public StatusValue maxHealth { get; protected set; }
        public IStatusValueFactory StatusValueFactory;

        public HealthStatus(float currentHealth, float maxHealth, StatusMediator statusMediator) {
            StatusValueFactory = new StatusValueFactory(statusMediator);
            this.currentHealth = StatusValueFactory.Create(nameof(this.currentHealth), currentHealth);
            this.maxHealth = StatusValueFactory.Create(nameof(this.maxHealth), maxHealth);
        }

        /// <summary>
        /// Reduces the current maxHealth by a specified damage amount.
        /// Ensures the maxHealth does not drop below 0.
        /// </summary>
        /// <param name="amount">The amount of damage taken.</param>
        public void TakeDamage(float amount) {
            currentHealth.CurrentValue = Mathf.Clamp(currentHealth.CurrentValue - amount, 0, maxHealth.CurrentValue);
        }

        /// <summary>
        /// Increases the current maxHealth by a specified amount.
        /// Ensures the maxHealth does not exceed the maximum maxHealth.
        /// </summary>
        /// <param name="amount">The amount of maxHealth to restore.</param>
        public void Heal(float amount) {
            currentHealth.CurrentValue = Mathf.Clamp(currentHealth.CurrentValue + amount, 0, maxHealth.CurrentValue);
        }

        /// <summary>
        /// Sets the current maxHealth to 0, effectively "killing" the character.
        /// </summary>
        public void Kill() {
            currentHealth.CurrentValue = 0;
        }
        
        /// <summary>
        /// Fully restores maxHealth to the maximum value.
        /// </summary>
        public void FullHeal() {
            currentHealth.CurrentValue = maxHealth.CurrentValue;
        }
    }
}
