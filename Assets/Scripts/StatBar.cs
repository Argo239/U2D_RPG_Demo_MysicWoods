using Assets.Scripts.Stats.PlayerStats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusBar : MonoBehaviour {
    public static PlayerStatusBar Instance { get; private set; }

    [SerializeField] private Image _healthTopBar;
    [SerializeField] private Image _healthBottomBar;
    [SerializeField] private Image _manaTopBar;
    [SerializeField] private Image _manaBottomBar;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _manaText;

    private ProgressBar _healthBar;
    private ProgressBar _manaBar;
    private PlayerAttributes _playerStat;


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

        // ��ʼ�� UI
        _healthTopBar.fillAmount = 0f;
        _healthBottomBar.fillAmount = 0f;
        _manaTopBar.fillAmount = 0f;
        _manaBottomBar.fillAmount = 0f;
    }

    private void Start() {
        _playerStat = PlayerStatus.Instance.attributes;
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
        // ���½�����
        _healthBar.Update(_healthValue, _healthMaxValue);
        _manaBar.Update(_manaValue, _manaMaxValue);
    }

    private void GetValue() {
        _healthValue = _playerStat.HealthStatus.currentHealth.CurrentValue;
        _healthMaxValue = _playerStat.HealthStatus.health.CurrentValue;

        _manaValue = _playerStat.ManaStatus.currentMana.CurrentValue;
        _manaMaxValue = _playerStat.ManaStatus.mana.CurrentValue;
    }

    private void UpdateUI() {
        // ���� UI ��ʾ
        _healthTopBar.fillAmount = _healthBar.topBarValue;
        _healthBottomBar.fillAmount = _healthBar.bottomBarValue;

        _manaTopBar.fillAmount = _manaBar.topBarValue;
        _manaBottomBar.fillAmount = _manaBar.bottomBarValue;

        _healthText.text = $"{_healthValue} / {_healthMaxValue}";
        _manaText.text = $"{_manaValue} / {_manaMaxValue}";
    }
}
