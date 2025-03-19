using Assets.Scripts.Interface;
using UnityEngine;
using static Argo_Utils.Utils;

public class WeaponSystem : MonoBehaviour {
    public static WeaponSystem Instance { get; private set; }

    [SerializeField] private bool consoleLogOn;
    [SerializeField] private MonoBehaviour weaponLogicComponent;
    private IWeaponLogic weaponLogic;

    private void Awake() {
        Instance = this;
        if (weaponLogicComponent != null) {
            weaponLogic = weaponLogicComponent as IWeaponLogic;
            if (weaponLogic == null) {
                DebugLog(consoleLogOn, $"Not implement IWeaponLogin");
            }
        }
    }

    private void Start() {
        GameInput.Instance.OnPlayerAttacking += GameInput_OnPlayerAttacking;
    }

    private void Update() {

    }

    private void GameInput_OnPlayerAttacking(object sender, System.EventArgs e) {
        if (weaponLogic != null) {
            weaponLogic.Attack();
        } else {
            DebugLog(consoleLogOn, "Cannot attack because weaponLogic is null!");
        }
    }
}

