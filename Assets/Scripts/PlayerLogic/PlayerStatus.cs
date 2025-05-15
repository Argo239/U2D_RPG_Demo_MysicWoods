using Assets.Scripts.Stats.StatsOperation;
using System;
using UnityEngine;
using static Argo_Utils.Utils;

public class PlayerStatus : MonoBehaviour {
    public static PlayerStatus Instance { get; private set; }

     [SerializeField] private PlayerAttributesInitData _playerAttributesInitData;

    private StatusModifier _statusModifier;
    private StatusMediator _statusMediator;
    private IStatusModifierFactory _statusModifierFactory;

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
        if (_playerAttributesInitData == null) {
            DebugLog(PlayerController.Instance.consoleLogOn, $"Player data init error");
            return;
        }
        attributes = new PlayerAttributes(_playerAttributesInitData, _statusMediator);
    }

    private void Start() {
        GameInput.Instance.OnPlayerRevive += GameInput_OnPlayerRevive;
    }

    private void Update() {
        //DebugLog(PlayerController.Instance._consoleLogOn,
        //    $"The base value of the current maxHealth: {attributes.HealthStatus.currentHealth.BaseValue}," +
        //    $"The current value of the current maxHealth: {attributes.CurrentHealth}," +
        //    $"Cache: {_statusMediator.GetLastQueryResult(attributes.HealthStatus.currentHealth.StatusType)}");
        TestStats();

        _statusMediator.Update(Time.deltaTime);
        attributes.Update();
        CheckPlayerIsDie();
    }

    private void GameInput_OnPlayerRevive(object sender, EventArgs e) => attributes.HealthStatus.FullHeal();

    public void TestStats() {
        if (Input.GetKeyDown(KeyCode.U)) {
            _statusModifier = _statusModifierFactory.Create(
                OperatorType.Add, attributes.HealthStatus.currentHealth.StatusType, 20, 10f);
            _statusMediator.AddModifier(_statusModifier);
            DebugLog(PlayerController.Instance.consoleLogOn, $"Click U");
        }
        if (Input.GetKeyDown(KeyCode.I)) {
            _statusModifier = _statusModifierFactory.Create(
                OperatorType.Add, attributes.HealthStatus.maxHealth.StatusType, 20f, 10f);
            _statusMediator.AddModifier(_statusModifier);
            DebugLog(PlayerController.Instance.consoleLogOn, $"Click I");
        }
        if (Input.GetKeyDown(KeyCode.O)) {
            _statusModifier = _statusModifierFactory.Create(
                OperatorType.Multiply, attributes.HealthStatus.currentHealth.StatusType, 0.2f, 10f);
            _statusMediator.AddModifier(_statusModifier);
            DebugLog(PlayerController.Instance.consoleLogOn, $"Click O");
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            if (_statusModifier != null) {
                _statusMediator.RemoveModifier(_statusModifier);
            }
            DebugLog(PlayerController.Instance.consoleLogOn, $"Click R");
        }
        if (Input.GetKeyDown(KeyCode.H)) {
            attributes.HealthStatus.Heal(10);
            DebugLog(PlayerController.Instance.consoleLogOn, $"Click H");
        }
        if (Input.GetKeyDown(KeyCode.G)) {
            attributes.HealthStatus.TakeDamage(10);
            DebugLog(PlayerController.Instance.consoleLogOn, $"Click G");
        }
        //if (Input.GetKeyDown(KeyCode.K)) {
        //    attributes.HealthStatus.Kill();
        //    DebugLog(PlayerController.Instance._consoleLogOn, $"Click K");
        //}
    }

    private void CheckPlayerIsDie() {
        float playerhealth = attributes.HealthStatus.currentHealth.CurrentValue;
        if (playerhealth <= 0) {
            if(PlayerController.Instance.GetCurrentState() != PlayerController.State.Dead) OnPlayerDeath.Invoke(this, EventArgs.Empty);
        }

    }
}
