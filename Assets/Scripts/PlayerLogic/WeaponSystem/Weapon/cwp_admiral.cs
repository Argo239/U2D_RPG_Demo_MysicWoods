using UnityEngine;

public class cwp_admiral : BaseWeaponLogic {
    public static cwp_admiral Instance { get; private set; }

    private PlayerController playerController;
    private CalculateHitboxRange calHitboxRange;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        calHitboxRange = new CalculateHitboxRange();
    }

    private void Start() {
        playerController = gameObject.GetComponentInParent<PlayerController>();
    }

    public override void AttackStep(ComboStepData step, Vector2 inputVector) {

    }

    public override void StopAttack() {
        throw new System.NotImplementedException();
    }
}