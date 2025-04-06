using UnityEngine;

public class PlayerAttack : MonoBehaviour { 
    public static PlayerAttack Instance { get; private set; }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
