using NUnit.Framework.Constraints;
using UnityEngine;

namespace Assets.Scripts.Stats.BaseStats {
    public class ManaStat {
        public StatValue Mana { get; protected set; }
        public StatValue CurrentMana {  get; protected set; }

        public ManaStat(float mana, float currentMana) {
            Mana = new StatValue("ManaStat", mana);
            CurrentMana = new StatValue("CurrentMana", currentMana);
        }

        public void UseMana(float amount) {
            CurrentMana.CurrentValue = Mathf.Clamp(CurrentMana.CurrentValue - amount, 0, Mana.CurrentValue);
        }

        public void ReplyMana(float amount) {
            CurrentMana.CurrentValue = Mathf.Clamp(CurrentMana.CurrentValue + amount, 0, Mana.CurrentValue);
        }

        public void RestoreFullMana(float amount) {
            CurrentMana.CurrentValue = Mana.CurrentValue;
        }

        public void UseAllMana() {
            CurrentMana.CurrentValue = 0;
        }
    }
}
