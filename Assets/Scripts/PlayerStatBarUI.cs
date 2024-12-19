using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBarUI : MonoBehaviour {
    public static PlayerStatBarUI Instance { get; private set; }

    [SerializeField] private Image _healthTopImage;
    [SerializeField] private Image _healthBottomImage;
    [SerializeField] private Image _experienceImage;

    private ProgressBar _healthBar;
    private ProgressBar _experienceBar;
    private PlayerAttributes _playerAttributes;
    private Material _material;

    private float _maxHealthValue;
    private float _currentHealthValue;
    private float _experience;
    private float _experienceToUpgrade;
    private float _segmentHealth;

    private void Awake() {
          
        if (Instance != null && Instance != this) {
            Destroy(Instance); 
            return;
        }
        Instance = this;

        // 初始化 UI
        _healthTopImage.fillAmount = 0f;
        _healthBottomImage.fillAmount = 0f;
        _experienceImage.fillAmount = 0f;
    }

    private void Start() {
        _playerAttributes = PlayerStatus.Instance.attributes;
        _material = _healthTopImage.material;
        InitializeBars();
    }

    private void Update() {
        UpdateStats();
        UpdateUI();
    }

    private void InitializeBars() {
        GetValue();
        _healthBar = new ProgressBar(_currentHealthValue, _maxHealthValue);
        _experienceBar = new ProgressBar(_experience, _experienceToUpgrade);
    }

    private void UpdateStats() {
        GetValue();
        // 更新进度条 
        _healthBar.Update(_currentHealthValue, _maxHealthValue);
        _experienceBar.Update(_experience, _experienceToUpgrade);
    }

    private void GetValue() {
        _currentHealthValue = _playerAttributes.CurrentHealth;
        _maxHealthValue = _playerAttributes.MaxHealth;

        _experience = _playerAttributes.Experience;
        _experienceToUpgrade = _playerAttributes.ExperienceToUpgrade;
    }

    private void UpdateUI() {
        // 更新 UI 显示
        _healthTopImage.fillAmount = _healthBar.topBarValue;
        _healthBottomImage.fillAmount = _healthBar.bottomBarValue;
        _experienceImage.fillAmount = _experienceBar.topBarValue;

        _segmentHealth = _maxHealthValue / 25f;
        _material.SetFloat("_segmentAmount", _segmentHealth);
    }
}
