using System;

namespace Assets.Scripts.Stats.StatsOperation {
    public enum ModifierType {
        Flat,
        Percentage
    }

    public class StatModifier : IDisposable {
        public StatType StatType { get; }
        public ModifierType ModifierType { get; }
        public float Value { get; }
        public bool MarkedForRemoval { get; set; }

        public event Action<StatModifier> OnDispose = delegate { };
        private CountdownTimer timer;

        public StatModifier(StatType type, ModifierType modifierType, float value, float duration = 0) {
            StatType = type;
            ModifierType = modifierType;
            Value = value;

            if (duration > 0) {
                timer = new CountdownTimer(duration);
                timer.OnTimerStop += () => MarkedForRemoval = true;
                timer.Start();
            }
        }

        public void Update(float deltaTime) => timer?.Tick(deltaTime);


        public void Dispose() => OnDispose.Invoke(this);
    }
}
