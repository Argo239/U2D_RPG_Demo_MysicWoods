using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsData", menuName = "PlayerStatsData/PlayerStats")]
public class PlayerStatsData : ScriptableObject {
    public string PlayerName;
    public float Health = 100;
    public float CurrentHealth = 100;
    public float Mana = 100;
    public float CurrentMana = 100;
    public float Attack = 5;
    public float DamageIncrease = 0f;
    public float Defense = 5;
    public float DamageDecrease = 0f;
    public float Speed = 5f;
    public int Weight = 1;
    public int Level = 1;
    public int Experience = 0;
    public int ExperienceToUpgrade = 100;
    public int baseExperienceUpgrade = 100;
}   