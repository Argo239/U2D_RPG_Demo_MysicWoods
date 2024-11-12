using Assets.Scripts.Stats.BaseStats;
using UnityEngine;

namespace Assets.Scripts.Stats.Stat {
    public class LevelStat {
        public StatValue Level { get; private set; }
        public int Experience { get; private set; }
        public int ExperienceToLevelUp { get; private set; }

        private int baseExperienceToLevelUp;
        private int increment = 20;

        public LevelStat(int level, int experience, int baseExperienceToLevelUp) {
            Level = new StatValue("LevelStat", level);
            Experience = experience;
            this.baseExperienceToLevelUp = baseExperienceToLevelUp;

            ExperienceToLevelUp = CalculateExperienceToLevelUp();
        }

        public void AddExperience(int amount) {
            Experience += amount;
            CheckForLevelUp();
        }

        private void CheckForLevelUp() {
            while (Experience >= ExperienceToLevelUp) {
                Experience -= ExperienceToLevelUp;
                LevelUp();
            }
        }

        private void LevelUp() {
            Level.BaseValue++;
            ExperienceToLevelUp = CalculateExperienceToLevelUp();
        }

        private int CalculateExperienceToLevelUp() {
            return Mathf.CeilToInt(baseExperienceToLevelUp + (Level.BaseValue - 1) * Mathf.Sqrt(Level.BaseValue - 1) * increment);
        }
    }
}
