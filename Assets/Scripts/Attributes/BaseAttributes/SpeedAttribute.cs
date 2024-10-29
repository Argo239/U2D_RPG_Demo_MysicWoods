namespace Assets.Scripts.Attributes.BaseAttributes {
    public class SpeedAttribute {
        public float Speed { get; private set; }
        public float SpeedMultiplier { get; private set; }
        
        public SpeedAttribute(float speed, float speedMultiplier = 1f) { 
            Speed = speed;
            SpeedMultiplier = speedMultiplier;
        }

        // 获取当前速度
        public float GetSpeed(bool isRunning) {
            return isRunning ? Speed * SpeedMultiplier : Speed;  // 根据跑步状态返回不同的速度
        }

        // 设置速度加成倍率
        public void SetSpeedMultiplier(float multiplier) {
            SpeedMultiplier = multiplier;
        }

        // 重置速度加成倍率为默认值
        public void ResetSpeedMultiplier() {
            SpeedMultiplier = 1f;
        }
    }
}
