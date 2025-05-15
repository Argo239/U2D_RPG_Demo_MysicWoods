using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour { 
    public static PlayerAttack Instance { get; private set; }
    
    public event EventHandler<AttackEventArgs> OnPlayerAttackPerformed;

    private PlayerController _playerController;
    private GameInput _gameInput;
    private WeaponSystem _weaponSystem;
    private Vector2 _inputVector;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        } 
        Instance = this;

        _playerController = PlayerController.Instance;
        _gameInput = _playerController.GameInput ;
        _weaponSystem = GetComponentInChildren<WeaponSystem>();
    }

    private void Update() {
        _inputVector = _playerController.GetInputVector();
    }

    private void OnEnable() {
        _gameInput.OnPlayerAttackPerformed += GameInput_OnPlayerAttackPerformed;
    }

    private void OnDisable() {
        _gameInput.OnPlayerAttackPerformed -= GameInput_OnPlayerAttackPerformed;
    }

    private void GameInput_OnPlayerAttackPerformed(object sender, System.EventArgs e) {
        _weaponSystem.PreformCombo(_inputVector);
        OnPlayerAttackPerformed.Invoke(this, new AttackEventArgs(_weaponSystem.CurrentComboStepData));
    }
}
