using UnityEngine;

public static class HitboxManager {
    public static void SpawnHitbox(GameObject gameObject, HitboxConfig config, Vector2 inputVector) {
        var colliderFactory = ColliderFactoryProvider.GetFactory(config);
        colliderFactory.CreateCollider(gameObject, config, inputVector);
    }
}