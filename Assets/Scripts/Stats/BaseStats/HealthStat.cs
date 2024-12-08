﻿using Assets.Scripts.Stats.StatsOperation;
using UnityEngine;

namespace Assets.Scripts.Stats.BaseStats {
    public class HealthStat {
        public StatValue health { get; protected set; }
        public StatValue currentHealth { get; protected set; }
        public IStatValueFactory StatValueFactory;

        public HealthStat(float health, float currentHealth, StatsMediator statsMediator) {
            StatValueFactory = new StatValueFactory(statsMediator);
            this.health = StatValueFactory.Create(nameof(this.health), health);
            this.currentHealth = StatValueFactory.Create(nameof(this.currentHealth), health);
        }

        /// <summary>
        /// Reduces the current health by a specified damage amount.
        /// Ensures the health does not drop below 0.
        /// </summary>
        /// <param name="amount">The amount of damage taken.</param>
        public void TakeDamage(float amount) {
            currentHealth.CurrentValue = Mathf.Clamp(currentHealth.CurrentValue - amount, 0, health.CurrentValue);
        }

        /// <summary>
        /// Increases the current health by a specified amount.
        /// Ensures the health does not exceed the maximum health.
        /// </summary>
        /// <param name="amount">The amount of health to restore.</param>
        public void Heal(float amount) {
            currentHealth.CurrentValue = Mathf.Clamp(currentHealth.CurrentValue + amount, 0, health.CurrentValue);
        }

        /// <summary>
        /// Sets the current health to 0, effectively "killing" the character.
        /// </summary>
        public void Kill() {
            currentHealth.CurrentValue = 0;
        }
        
        /// <summary>
        /// Fully restores health to the maximum value.
        /// </summary>
        public void FullHeal() {
            currentHealth.CurrentValue = health.CurrentValue;
        }
    }
}
