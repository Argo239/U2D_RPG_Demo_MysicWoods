using Assets.Scripts.Attributes.BaseAttributes;
using UnityEngine;

namespace Assets.Scripts.Attributes.CharacterAttributes {
    public class PlayerAttributes : BaseCharacterAttributes {
        public int Level { get; set; }
        public int Experience { get; set; }

        public PlayerAttributes(HPAttribute hpAttribute, MPAttribute mpAttribute, ATKAttribute atkAttribute, DEFAttribute defAttribute, SPDAttribute spdAttribute, int level, int experience)
            : base(hpAttribute, mpAttribute, atkAttribute, defAttribute, spdAttribute) {
            Level = level;
            Experience = experience;
        }

        protected override void OnDeath() {
            // Player was died...
            base.OnDeath();
        }
    }
}
