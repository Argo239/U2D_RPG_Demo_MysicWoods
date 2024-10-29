using UnityEngine.UIElements;

namespace Interface {
    public interface IBaseAttributes {
        float MaxHealth { get; set; }
        float CurrentHealth { get; set; }
        float MaxMana { get; set; }
        float CurrentMana { get; set; }
        float Attack {  get; set; }
        float DamageBuff { get; set; }
        float Defense { get; set; }
        float DamageReduction { get; set; }
        float AttackSpeed { get; set; }
        float Speed { get; set; }   
    }
}