using System.Collections;
using UnityEngine;

public class Hitbox : MonoBehaviour {
    [HideInInspector] public float lifetime = .5f;

    private void Start() {
        StartCoroutine(SelfDestroy());
    }

    private IEnumerator SelfDestroy() {
        yield return new WaitForSeconds(lifetime);
        Destroy(this);
    }
}