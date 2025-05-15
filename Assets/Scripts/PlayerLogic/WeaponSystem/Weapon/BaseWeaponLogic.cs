using UnityEngine;

public abstract class BaseWeaponLogic : MonoBehaviour {
    [Header("Weapon Data / 武器數據")]
    public WeaponData WeaponData;

    public abstract void AttackStep(ComboStepData step, Vector2 inputVector);
    public abstract void StopAttack();
}