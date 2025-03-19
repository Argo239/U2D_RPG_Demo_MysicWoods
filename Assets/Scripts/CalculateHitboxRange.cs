using UnityEngine;

public class CalculateHitboxRange {
    public Vector2[] GenerateSector(SectorHitboxRangeConfig config, Vector2 facingDir) {
        // Total points: outer arc (pointCount+1) + inner arc (pointCount+1)
        Vector2[] points = new Vector2[(config.pointCount + 1) * 2];

        // Calculate base angle from the facing direction
        float baseAngle = Mathf.Atan2(facingDir.y, facingDir.x) * Mathf.Rad2Deg;
        float startAngle = -config.angle / 2f;
        float angleStep = config.angle / config.pointCount;

        // Generate outer arc points
        for (int i = 0; i <= config.pointCount; i++) {
            float currentAngle = baseAngle + startAngle + angleStep * i;
            float rad = currentAngle * Mathf.Deg2Rad;
            points[i] = new Vector2(Mathf.Cos(rad) * config.outerRadius, Mathf.Sin(rad) * config.outerRadius);
        }

        // Generate inner arc points (in reverse order to form a closed polygon)
        for (int i = 0; i <= config.pointCount; i++) {
            float currentAngle = baseAngle + startAngle + angleStep * (config.pointCount - i);
            float rad = currentAngle * Mathf.Deg2Rad;
            points[config.pointCount + 1 + i] = new Vector2(Mathf.Cos(rad) * config.innerRadius, Mathf.Sin(rad) * config.innerRadius);
        }

        return points;
    }
}
