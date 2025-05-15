using UnityEngine;

public class CalculateHitboxRange {
    /// <summary>
    /// Generate the sector hitbox data
    /// 生成扇形碰撞體數據
    /// </summary>
    /// <param name="config"></param>
    /// <param name="facingDir"></param>
    /// <returns></returns>
    public Vector2[] GenerateSector(SectorHitboxRangeConfig config, Vector2 facingDir) {
        // Total points: outer arc (PointCount+1) + inner arc (PointCount+1)
        Vector2[] points = new Vector2[(config.PointCount + 1) * 2];

        // Calculate base Angle from the facing direction
        float baseAngle = Mathf.Atan2(facingDir.y, facingDir.x) * Mathf.Rad2Deg;
        float startAngle = -config.Angle / 2f;
        float angleStep = config.Angle / config.PointCount;

        // Generate outer arc points
        for (int i = 0; i <= config.PointCount; i++) {
            float currentAngle = baseAngle + startAngle + angleStep * i;
            float rad = currentAngle * Mathf.Deg2Rad;
            points[i] = new Vector2(Mathf.Cos(rad) * config.OuterRadius, Mathf.Sin(rad) * config.OuterRadius);
        }

        // Generate inner arc points (in reverse order to form a closed polygon)
        for (int i = 0; i <= config.PointCount; i++) {
            float currentAngle = baseAngle + startAngle + angleStep * (config.PointCount - i);
            float rad = currentAngle * Mathf.Deg2Rad;
            points[config.PointCount + 1 + i] = new Vector2(Mathf.Cos(rad) * config.InnerRadius, Mathf.Sin(rad) * config.InnerRadius);
        }

        return points;
    }

    /// <summary>
    /// Generate the Circle hitbox data
    /// 生產圓形碰撞體數據
    /// </summary>
    public Vector2[] GenerateCircle(CircleHitboxRangeConfig config) {
        int count = config.segmentCount;

        Vector2[] points = new Vector2[count + 1];

        float angleStep = 360f / count;
        for (int i = 0; i <= count; i++) {
            float angleDeg = angleStep * i;
            float rad = angleDeg * Mathf.Deg2Rad;
            points[i] = new Vector2(
                Mathf.Cos(rad) * config.radius + config.offset.x,
                Mathf.Sin(rad) * config.radius + config.offset.y
            );
        }
        return points;
    }
}
