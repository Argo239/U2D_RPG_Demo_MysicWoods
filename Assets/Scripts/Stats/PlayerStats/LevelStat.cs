using Assets.Scripts.Stats.BaseStats;
using Assets.Scripts.Stats.StatsOperation;
using UnityEngine;

namespace Assets.Scripts.Stats.Stat {
    public class LevelStat {
        public StatValue level { get; private set; }
        public int experience { get; private set; }
        public int experienceToUpgrade { get; private set; }

        private int baseExperienceUpgrade;
        private int increment = 20;

        public StatValueFactory StatValueFactory;

        public LevelStat(int level, int experience, int baseExperienceToUpgrade, StatsMediator statsMediator) {
            StatValueFactory = new StatValueFactory(statsMediator);
            this.level = StatValueFactory.Create(nameof(this.level), level);
            this.experience = experience;
            this.baseExperienceUpgrade = baseExperienceToUpgrade;

            experienceToUpgrade = CalculateExperienceToUpgrade();
        }

        public void AddExperience(int amount) {
            experience += amount;
            CheckForUpgrade();
        }

        private void CheckForUpgrade() {
            while (experience >= experienceToUpgrade) {
                experience -= experienceToUpgrade;
                LevelUp();
            }
        }

        private void LevelUp() {
            level.BaseValue++;
            experienceToUpgrade = CalculateExperienceToUpgrade();
        }

        private int CalculateExperienceToUpgrade() {
            return Mathf.CeilToInt(baseExperienceUpgrade + (level.BaseValue - 1) * Mathf.Sqrt(level.BaseValue - 1) * increment);
        }
    }
}
