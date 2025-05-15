using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject {
    [Header("Name of weapon/ 武器名稱")]
    public string Name = "Default";

    [Header("Damage / 傷害")]
    public float BaseDamage = 10f;

    [Header("Combo Data List/ 連招段列表")]
    public ComboListData ComboListData;
}