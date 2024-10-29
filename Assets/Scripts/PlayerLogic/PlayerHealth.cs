using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    private double maxHealth = 100;
    private double currentHealth;

    private event System.Action OnPlayerDeath;
    private event System.Action<float> OnPlayerChanged;

    private void Start() {
        currentHealth = maxHealth;
    }

}

