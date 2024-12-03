using Assets.Scripts.Stats;
using Assets.Scripts.Stats.BaseStats;
using Assets.Scripts.Stats.Stat;
using Assets.Scripts.Stats.StatsOperation;

namespace Assets.Scripts.Stats.PlayerStats {
    public class PlayerStat : BaseStat {
        public string PlayerName { get; private set; }

        public LevelStat levelStat { get; private set; }

        public PlayerStat(PlayerStatsData playerStatsData, StatsMediator statsMediator)
            : base(
                new HealthStat(playerStatsData.Health, playerStatsData.CurrentHealth, statsMediator),
                new ManaStat(playerStatsData.Mana, playerStatsData.CurrentMana,  statsMediator),
                new AttackStat(playerStatsData.Attack, playerStatsData.DamageIncrease, statsMediator),
                new DefenseStat(playerStatsData.Defense, playerStatsData.DamageDecrease, statsMediator),
                new SpeedStat(playerStatsData.Speed, statsMediator),
                new WeightStat(playerStatsData.Weight, statsMediator)
            ) {
            PlayerName = playerStatsData.PlayerName;
            levelStat = new LevelStat(playerStatsData.Level, playerStatsData.Experience, playerStatsData.baseExperienceUpgrade, statsMediator);
        }
    }
}
