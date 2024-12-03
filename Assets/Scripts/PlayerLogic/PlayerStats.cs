using Assets.Scripts.Stats.PlayerStats;
using Assets.Scripts.Stats.StatsOperation;
using UnityEngine;
using static Argo_Utils.Utils;

public class PlayerStats : MonoBehaviour {
    public static PlayerStats Instance { get; private set; }

    public PlayerStat playerStat;

    private StatModifier statModifier;
    private StatsMediator statsMediator;
    private IStatModifierFactory statModifierFactory;

    public string playerName;
    [SerializeField] private PlayerStatsData playerStatsData;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start() {
        statsMediator = new StatsMediator();
        statModifierFactory = new StatModifierFactory();
        playerStat = new PlayerStat(playerStatsData, statsMediator);
    }

    private void Update() {
        LogMessage(PlayerController.Instance.consoleLogOn,
            $"The base value of the current health: {playerStat.HealthStat.currentHealth.BaseValue}," +
            $"The current value of the current health: {playerStat.HealthStat.currentHealth.CurrentValue}," +
            $"Cache: {statsMediator.GetLastQueryResult(playerStat.HealthStat.currentHealth.StatType)}");
        TestStats();

        statsMediator.Update(Time.deltaTime);
    }

    public void TestStats() {
        if (Input.GetKeyDown(KeyCode.U)) {
            statModifier = statModifierFactory.Create(
                OperatorType.Add, playerStat.HealthStat.currentHealth.StatType, 20, 10f);
            statsMediator.AddModifier(statModifier);
            LogMessage(PlayerController.Instance.consoleLogOn, $"Click U");
        }
        if (Input.GetKeyDown(KeyCode.I)) {
            statModifier = statModifierFactory.Create(
                OperatorType.Multiply, playerStat.HealthStat.currentHealth.StatType, 0.2f, 10f);
            statsMediator.AddModifier(statModifier);
            LogMessage(PlayerController.Instance.consoleLogOn, $"Click I");
        }
        if (Input.GetKeyDown(KeyCode.O)) {
            statModifier = statModifierFactory.Create(
                OperatorType.Multiply, playerStat.HealthStat.currentHealth.StatType, 0.8f, 10f);
            statsMediator.AddModifier(statModifier);
            LogMessage(PlayerController.Instance.consoleLogOn, $"Click O");
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            if (statModifier != null) {
                statsMediator.RemoveModifier(statModifier);
            }
            LogMessage(PlayerController.Instance.consoleLogOn, $"Click R");
        }
        if (Input.GetKeyDown(KeyCode.H)) {
            playerStat.HealthStat.Heal(10);
            LogMessage(PlayerController.Instance.consoleLogOn, $"Click H");
        }
        if (Input.GetKeyDown(KeyCode.G)) {
            playerStat.HealthStat.TakeDamage(10);
            LogMessage(PlayerController.Instance.consoleLogOn, $"Click G");
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            playerStat.HealthStat.Kill();
            LogMessage(PlayerController.Instance.consoleLogOn, $"Click K");
        }
    }
}
