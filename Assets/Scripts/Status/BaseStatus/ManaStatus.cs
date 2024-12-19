using Assets.Scripts.Stats.StatsOperation;
using NUnit.Framework.Constraints;
using UnityEngine;

namespace Assets.Scripts.Stats.BaseStatus {
    public class ManaStatus {
        public StatusValue currentMana { get; protected set; }
        public StatusValue maxMana { get; protected set; }
        public IStatusValueFactory StatusValueFactory;

        public ManaStatus(float currentMana, float maxMana, StatusMediator statusMediator) {
            StatusValueFactory = new StatusValueFactory(statusMediator);
            this.currentMana = StatusValueFactory.Create(nameof(this.currentMana), currentMana);
            this.maxMana = StatusValueFactory.Create(nameof(this.maxMana), maxMana);
        }

        public void UseMana(float amount) {
            currentMana.CurrentValue = Mathf.Clamp(currentMana.CurrentValue - amount, 0, maxMana.CurrentValue);
        }

        public void ReplyMana(float amount) {
            currentMana.CurrentValue = Mathf.Clamp(currentMana.CurrentValue + amount, 0, maxMana.CurrentValue);
        }

        public void RestoreFullMana(float amount) {
            currentMana.CurrentValue = maxMana.CurrentValue;
        }

        public void UseAllMana() {
            currentMana.CurrentValue = 0;
        }
    }
}
