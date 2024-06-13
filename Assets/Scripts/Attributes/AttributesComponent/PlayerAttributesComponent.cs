using Assets.Scripts.Attributes.CharacterAttributes;
using UnityEngine;
using Assets.Scripts.Attributes.BaseAttributes;

namespace Assets.Scripts.Attributes.AttributesComponent {
    public class PlayerAttributesComponent : MonoBehaviour {
        public static PlayerAttributesComponent Instance {  get; private set; }
        public PlayerAttributes PlayerAttributes { get; private set; }

        private void Awake() {
            Instance = this;
        }

        private void Start() {
            PlayerAttributes = new PlayerAttributes(
                new HPAttribute(100, 100),
                new MPAttribute(100, 100),
                new ATKAttribute(5),
                new DEFAttribute(2, 0.1f),
                new SPDAttribute(5, 2),
                1,
                0
            );
        }
    }
}
