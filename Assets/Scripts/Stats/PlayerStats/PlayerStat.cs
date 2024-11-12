using Assets.Scripts.Stats;
using Assets.Scripts.Stats.BaseStats;
using Assets.Scripts.Stats.Stat;

namespace Assets.Scripts.Stats.PlayerStats {
    public class PlayerStat : BaseStat {
        public string PlayerName { get; private set; }
        public LevelStat LevelStat { get; private set; }

        public PlayerStat(HealthStat healthStat, ManaStat manaStat, AttackStat attackStat, DefenseStat defenseStat, SpeedStat speedStat, WeightStat weightStat, LevelStat levelStat, string playerName) : base(healthStat, manaStat, attackStat, defenseStat, speedStat, weightStat) {
            PlayerName = playerName;
            LevelStat = levelStat;
        }
    }
}
