namespace Interface {
    public interface ICharacterAttributes {
        float HP { get; set; }
        float MaxHP { get; set; }
        float MP { get; set; }
        float MaxMP { get; set; }
        float ATK { get; set; }
        float DEF { get; set; }
        float DR { get; set; }
        float SPD { get; set; }
        float SPD_MULT { get; set; }
        void TakeDamage(float damage);
        void Heal(float amount);
        void UseMP(float amount);
        void ReplyMP(float amount);
    }
}