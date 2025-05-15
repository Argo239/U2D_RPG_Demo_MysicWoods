using UnityEngine;

public abstract class BaseCollider {
    public abstract Collider2D CreateCollider(GameObject gameObject, HitboxConfig config, Vector2 spawnDir);
}
