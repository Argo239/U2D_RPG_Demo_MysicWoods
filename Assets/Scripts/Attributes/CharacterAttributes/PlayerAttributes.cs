using Assets.Scripts.Attributes.BaseAttributes;
using Interface;
using UnityEngine;

namespace Assets.Scripts.Attributes.CharacterAttributes {
    public class PlayerAttributes {

        public HealthAttribute HealthAttribute { get; set; }
        public ManaAttribute ManaAttribute { get; set; }
        public AttackAttribute AttackAttribute { get; set; }
        public DefenseAttribute DefenseAttribute { get; set; }
        public SpeedAttribute SpeedAttribute { get; set; }

        public int Level { get; private set; }
        public int Experience { get; private set; }
        private int _experienceToLevelUp;

        public PlayerAttributes(HealthAttribute healthAttribute, ManaAttribute manaAttribute, AttackAttribute attackAttribute
            , DefenseAttribute defenseAttribute, SpeedAttribute speedAttribute, int level = 1, int experience = 0, int experienceToLevelUp = 100) {
            HealthAttribute = healthAttribute;
            ManaAttribute = manaAttribute;
            AttackAttribute = attackAttribute;
            DefenseAttribute = defenseAttribute;
            SpeedAttribute = speedAttribute;
            Level = level;
            Experience = experience;
            _experienceToLevelUp = experienceToLevelUp;
        }

        public void AddExperience(int amount) {
            Experience += amount;
            CheckForLevelUp();
        }

        private void CheckForLevelUp() {
            while (Experience >= _experienceToLevelUp) {
                Experience -= _experienceToLevelUp;
                LevelUp();
            }
        }

        private void LevelUp() {
            Level++;
            if (Level < 20)
                _experienceToLevelUp += 100;
            else if (Level >= 20)
                _experienceToLevelUp += 200;
        }
    }
}
