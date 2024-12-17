using Assets.Scripts.Stats.BaseStatus;
using Assets.Scripts.Stats.Stat;
using Assets.Scripts.Stats.StatsOperation;
using UnityEngine.Experimental.Rendering;

namespace Assets.Scripts.Stats.PlayerStats {
    public class PlayerAttributes : global::BaseStatus {
        public string PlayerName { get; private set; }
        public LevelStatus LevelStatus { get; protected set; }

        public PlayerAttributes(PlayerAttributesData initialData, StatusMediator statsMediator)
            : base(
                new HealthStatus(initialData.Health, initialData.CurrentHealth, statsMediator),
                new ManaStatus(initialData.Mana, initialData.CurrentMana,  statsMediator),
                new AttackStatus(initialData.Attack, initialData.DamageIncrease, statsMediator),
                new DefenseStatus(initialData.Defense, initialData.DamageDecrease, statsMediator),
                new SpeedStatus(initialData.Speed, statsMediator),
                new WeightStatus(initialData.Weight, statsMediator)
            ) {
            PlayerName = initialData.PlayerName;
            LevelStatus = new LevelStatus(initialData.Level, initialData.Experience, initialData.baseExperienceUpgrade, statsMediator);
        }
    }
}
