using Assets.Scripts.Stats.StatsOperation;
using NUnit.Framework.Constraints;
using UnityEngine;

namespace Assets.Scripts.Stats.BaseStats {
    public class ManaStat {
        public StatValue mana { get; protected set; }
        public StatValue currentMana { get; protected set; }
        public IStatValueFactory StatValueFactory;

        public ManaStat(float mana, float currentMana, StatsMediator statsMediator) {
            StatValueFactory = new StatValueFactory(statsMediator);
            this.mana = StatValueFactory.Create(nameof(this.mana), mana);
            this.currentMana = StatValueFactory.Create(nameof(this.currentMana), currentMana);
        }

        public void UseMana(float amount) {
            currentMana.CurrentValue = Mathf.Clamp(currentMana.CurrentValue - amount, 0, mana.CurrentValue);
        }

        public void ReplyMana(float amount) {
            currentMana.CurrentValue = Mathf.Clamp(currentMana.CurrentValue + amount, 0, mana.CurrentValue);
        }

        public void RestoreFullMana(float amount) {
            currentMana.CurrentValue = mana.CurrentValue;
        }

        public void UseAllMana() {
            currentMana.CurrentValue = 0;
        }
    }
}
