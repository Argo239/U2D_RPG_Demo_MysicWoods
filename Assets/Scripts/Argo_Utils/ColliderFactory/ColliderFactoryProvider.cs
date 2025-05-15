using System;
using System.Collections.Generic;

public static class ColliderFactoryProvider {
    private static readonly Dictionary<Type, BaseCollider> _colliderMap = new Dictionary<Type, BaseCollider> {
        { typeof(SectorHitboxRangeConfig), new SectorCollider2DFactory() },
        { typeof(CircleHitboxRangeConfig), new CircleCollider2DFactor() },
    };

    public static BaseCollider GetFactory(HitboxConfig config) { 
        var t = config.GetType();
        if(_colliderMap.TryGetValue(t, out BaseCollider collider)) return collider; 
        throw new ArgumentException($"No factory registered for {t}");
    }
}