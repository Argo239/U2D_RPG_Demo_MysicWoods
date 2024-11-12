using Assets.Scripts.Stats.BaseStats;
using Assets.Scripts.Stats.PlayerStats;
using Assets.Scripts.Stats.Stat;
using Assets.Scripts.Stats.StatsOperation;
using UnityEngine;
using static Argo_Utils.Utils;

public class PlayerStats : MonoBehaviour {
    public static PlayerStats Instance { get; private set; }
    public PlayerStat playerStat;
    private StatsMediator statsMediator;
    public string playerName;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Start() {
        statsMediator = new StatsMediator();
        playerStat = new PlayerStat(
            new HealthStat(100, 100),
            new ManaStat(100, 100),
            new AttackStat(5, 0f),
            new DefenseStat(5, 0f),
            new SpeedStat(5f),
            new WeightStat(1),
            new LevelStat(1, 0, 100),
            playerName
        );
    }


    private void Update() {
        LogMessage(PlayerController.Instance.consoleLogOn, playerStat.HealthStat.CurrentHealth.ToString());
        TestStats();
    }

    public void TestStats() {
        if (Input.GetKeyDown(KeyCode.K)) {

        }
        if (Input.GetKeyDown(KeyCode.H)) {
            playerStat.HealthStat.CurrentHealth.ApplyPerManentGain(20);

            StatModifier tempPercentModifier = new StatModifier(
                playerStat.HealthStat.CurrentHealth.StatType, ModifierType.Percentage, 0.2f, 10f
            );

            statsMediator.AddModifier(tempPercentModifier);
        }
    }
}
