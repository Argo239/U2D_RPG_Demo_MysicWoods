using Assets.Scripts.Stats.PlayerStats;
using Assets.Scripts.Stats.StatsOperation;
using System;
using UnityEngine;
using static Argo_Utils.Utils;

public class PlayerStatus : MonoBehaviour {
    public static PlayerStatus Instance { get; private set; }

    [SerializeField] private PlayerAttributesData _playerAttributesData;

    private StatusModifier _statusModifier;
    private StatusMediator _statusMediator;
    private IStatusModifierFactory _statusModifierFactory;

    public string playerName;
    public PlayerAttributes attributes;
    public event EventHandler OnPlayerDeath;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _statusMediator = new StatusMediator();
        _statusModifierFactory = new StatusModifierFactory();
        attributes = new PlayerAttributes(_playerAttributesData, _statusMediator);
    }

    private void Start() {

    }

    private void Update() {
        LogMessage(PlayerController.Instance.consoleLogOn,
            $"The base value of the current health: {attributes.HealthStatus.currentHealth.BaseValue}," +
            $"The current value of the current health: {attributes.HealthStatus.currentHealth.CurrentValue}," +
            $"Cache: {_statusMediator.GetLastQueryResult(attributes.HealthStatus.currentHealth.StatusType)}");
        TestStats();

        _statusMediator.Update(Time.deltaTime);
        CheckPlayerIsDie();
    }

    public void TestStats() {
        if (Input.GetKeyDown(KeyCode.U)) {
            _statusModifier = _statusModifierFactory.Create(
                OperatorType.Add, attributes.HealthStatus.currentHealth.StatusType, 20, 10f);
            _statusMediator.AddModifier(_statusModifier);
            LogMessage(PlayerController.Instance.consoleLogOn, $"Click U");
        }
        if (Input.GetKeyDown(KeyCode.I)) {
            _statusModifier = _statusModifierFactory.Create(
                OperatorType.Add, attributes.HealthStatus.health.StatusType, 20f, 10f);
            _statusMediator.AddModifier(_statusModifier);
            LogMessage(PlayerController.Instance.consoleLogOn, $"Click I");
        }
        if (Input.GetKeyDown(KeyCode.O)) {
            _statusModifier = _statusModifierFactory.Create(
                OperatorType.Multiply, attributes.HealthStatus.currentHealth.StatusType, 0.2f, 10f);
            _statusMediator.AddModifier(_statusModifier);
            LogMessage(PlayerController.Instance.consoleLogOn, $"Click O");
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            if (_statusModifier != null) {
                _statusMediator.RemoveModifier(_statusModifier);
            }
            LogMessage(PlayerController.Instance.consoleLogOn, $"Click R");
        }
        if (Input.GetKeyDown(KeyCode.H)) {
            attributes.HealthStatus.Heal(10);
            LogMessage(PlayerController.Instance.consoleLogOn, $"Click H");
        }
        if (Input.GetKeyDown(KeyCode.G)) {
            attributes.HealthStatus.TakeDamage(10);
            LogMessage(PlayerController.Instance.consoleLogOn, $"Click G");
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            attributes.HealthStatus.Kill();
            LogMessage(PlayerController.Instance.consoleLogOn, $"Click K");
        }
    }

    private void CheckPlayerIsDie() {
        float playerhealth = attributes.HealthStatus.currentHealth.CurrentValue;
        if (playerhealth <= 0) OnPlayerDeath.Invoke(this, EventArgs.Empty); 

    }
}
