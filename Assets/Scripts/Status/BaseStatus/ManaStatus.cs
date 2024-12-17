using Assets.Scripts.Stats.StatsOperation;
using NUnit.Framework.Constraints;
using UnityEngine;

namespace Assets.Scripts.Stats.BaseStatus {
    public class ManaStatus {
        public StatusValue mana { get; protected set; }
        public StatusValue currentMana { get; protected set; }
        public IStatusValueFactory StatusValueFactory;

        public ManaStatus(float mana, float currentMana, StatusMediator statusMediator) {
            StatusValueFactory = new StatusValueFactory(statusMediator);
            this.mana = StatusValueFactory.Create(nameof(this.mana), mana);
            this.currentMana = StatusValueFactory.Create(nameof(this.currentMana), currentMana);
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
