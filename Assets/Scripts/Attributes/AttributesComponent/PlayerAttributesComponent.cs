using Assets.Scripts.Attributes.CharacterAttributes;
using UnityEngine;
using Assets.Scripts.Attributes.BaseAttributes;

namespace Assets.Scripts.Attributes.AttributesComponent {
    public class PlayerAttributesComponent : MonoBehaviour {
        public static PlayerAttributesComponent Instance {  get; private set; }
        public PlayerAttributes PlayerAttributes { get; private set; }

        private void Awake() {
            if(Instance != null & Instance != this) 
                Destroy(this.gameObject);
            Instance = this; 
        }

        private void Start() {
            PlayerAttributes = new PlayerAttributes(
                new HealthAttribute(100, 100),
                new ManaAttribute(100, 100),
                new AttackAttribute(5),
                new DefenseAttribute(2, 0.1f),
                new SpeedAttribute(3),
                1,
                0
            );
        }
    }
}
