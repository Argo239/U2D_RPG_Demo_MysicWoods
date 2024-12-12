using Assets.Scripts.Stats.PlayerStats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Argo_Utils.Utils;

public class PlayerStatBar : MonoBehaviour {
    public static PlayerStatBar Instance { get; private set; }

    [SerializeField] private Image _healthTopBar;
    [SerializeField] private Image _healthBottomBar;
    [SerializeField] private Image _manaTopBar;
    [SerializeField] private Image _manaBottomBar;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _manaText;

    private ProgressBar _healthBar;
    private ProgressBar _manaBar;
    private PlayerStat _playerStat;


    private float _healthValue;
    private float _healthMaxValue;
    private float _manaValue;
    private float _manaMaxValue;

    private void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(Instance); 
            return;
        }
        Instance = this;

        // 初始化 UI
        _healthTopBar.fillAmount = 0f;
        _healthBottomBar.fillAmount = 0f;
        _manaTopBar.fillAmount = 0f;
        _manaBottomBar.fillAmount = 0f;
    }

    private void Start() {
        _playerStat = PlayerStats.Instance.playerStat;
        InitializeBars();
    }

    private void Update() {
        UpdateStats();
        UpdateUI();
    }

    private void InitializeBars() {
        GetValue();
        _healthBar = new ProgressBar(_healthValue, _healthMaxValue);
        _manaBar = new ProgressBar(_manaValue, _manaMaxValue);
    }

    private void UpdateStats() {
        GetValue();
        // 更新进度条
        _healthBar.Update(_healthValue, _healthMaxValue);
        _manaBar.Update(_manaValue, _manaMaxValue);
    }

    private void GetValue() {
        _healthValue = _playerStat.HealthStat.currentHealth.CurrentValue;
        _healthMaxValue = _playerStat.HealthStat.health.CurrentValue;

        _manaValue = _playerStat.ManaStat.currentMana.CurrentValue;
        _manaMaxValue = _playerStat.ManaStat.mana.CurrentValue;
    }

    private void UpdateUI() {
        // 更新 UI 显示
        _healthTopBar.fillAmount = _healthBar.topBarValue;
        _healthBottomBar.fillAmount = _healthBar.bottomBarValue;

        _manaTopBar.fillAmount = _manaBar.topBarValue;
        _manaBottomBar.fillAmount = _manaBar.bottomBarValue;

        _healthText.text = $"{_healthValue} / {_healthMaxValue}";
        _manaText.text = $"{_manaValue} / {_manaMaxValue}";
    }
}
