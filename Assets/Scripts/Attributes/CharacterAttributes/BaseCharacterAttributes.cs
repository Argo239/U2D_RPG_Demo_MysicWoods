using Assets.Scripts.Attributes.BaseAttributes;
using Interface;

public class BaseCharacterAttributes : ICharacterAttributes {
    public HPAttribute HPAttribute { get; private set; }
    public MPAttribute MPAttribute { get; private set; }
    public ATKAttribute ATKAttribute { get; private set; }
    public DEFAttribute DEFAttribute { get; private set; }
    public SPDAttribute SPDAttribute { get; private set; }

    public BaseCharacterAttributes(HPAttribute hpAttribute, MPAttribute mpAttribute,
        ATKAttribute atkAttribute, DEFAttribute defAttribute, SPDAttribute spdAttribute) {
        HPAttribute = hpAttribute; MPAttribute = mpAttribute;
        ATKAttribute = atkAttribute; DEFAttribute = defAttribute; SPDAttribute = spdAttribute;
    }

    public float HP {
        get => HPAttribute.HP;
        set => HPAttribute.HP = value;
    }

    public float MaxHP {
        get => HPAttribute.MaxHP;
        set => HPAttribute.MaxHP = value;
    }

    public float MP {
        get => MPAttribute.MP;
        set => MPAttribute.MP = value;
    }

    public float MaxMP {
        get => MPAttribute.MaxMP;
        set => MPAttribute.MaxMP = value;
    }

    public float ATK {
        get => ATKAttribute.ATK;
        set => ATKAttribute.ATK = value;
    }

    public float DEF {
        get => DEFAttribute.DEF;
        set => DEFAttribute.DEF = value;
    }

    public float DR {
        get => DEFAttribute.DR;
        set => DEFAttribute.DR = value;
    }

    public float SPD {
        get => SPDAttribute.SPD;
        set => SPDAttribute.SPD = value;
    }

    public float SPD_MULT {
        get => SPDAttribute.SPD_MULT;
        set => SPDAttribute.SPD_MULT = value;
    }

    public void TakeDamage(float amount) { HPAttribute.TakeDamage(amount); }

    public void Heal(float amount) { HPAttribute.Heal(amount); }

    public void UseMP(float amount) { MPAttribute.UseMP(amount); }

    public void ReplyMP(float amount) { MPAttribute.ReplyMP(amount); }

    protected virtual void OnDeath() {
        if (HP == 0) {
            //Player was died
        }
    }
}
