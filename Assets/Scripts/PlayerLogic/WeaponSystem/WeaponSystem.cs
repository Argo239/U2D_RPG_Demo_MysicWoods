using System.Collections;
using UnityEngine;
using static Argo_Utils.Utils;

[RequireComponent(typeof(BaseWeaponLogic))] 
public class WeaponSystem : MonoBehaviour {
    public static WeaponSystem Instance { get; private set; }

    [SerializeField] private bool _consoleLogOn;

    public ComboStepData CurrentComboStepData { get; private set; }

    private PlayerController _playerController;
    private PlayerAttack _playerAttack;
    private BaseWeaponLogic _weaponLogic;
    private WeaponData _currentWeaponData;
    private ComboManager _comboManager;
    private int _currentComboStepCount;


    private void Awake() {
        Instance = this;

        _playerController = PlayerController.Instance;
        _playerAttack = _playerController.PlayerAttack;

        _weaponLogic = GetComponentInChildren<BaseWeaponLogic>();
        if(_weaponLogic == null) {
            Debug.LogError("WeaponSystem: didn't found any weapon");
            return;
        } 

        _currentWeaponData = _weaponLogic.WeaponData;
        _comboManager = new ComboManager(_currentWeaponData.ComboListData);
    }

    private void OnEnable() {
        _playerAttack.OnPlayerAttackPerformed += PlayerAttack_OnPlayerAttackPerformed;
    }

    private void OnDisable() {
        _playerAttack.OnPlayerAttackPerformed -= PlayerAttack_OnPlayerAttackPerformed;
    }

    private void PlayerAttack_OnPlayerAttackPerformed(object sender, AttackEventArgs e) {
        PreformCombo(_playerController.GetInputVector());
    }

     public void PreformCombo(Vector2 inputVector) {
        if (!_comboManager.TryNext()) _comboManager.StartCombo();
        CurrentComboStepData = _comboManager._CurrentStep;

        if (CurrentComboStepData == null) {
            DebugLog(_consoleLogOn, "No combo CurrentComboStepData available");
            return;
        }

        StartCoroutine(ExecuteComboStep(CurrentComboStepData));
    }

    private IEnumerator ExecuteComboStep(ComboStepData step) {
        yield return new WaitForSeconds(step.StiffDuration);

        Vector2 inputVector = _playerController.GetInputVector();
        _weaponLogic.AttackStep(step, inputVector);

    }
}

