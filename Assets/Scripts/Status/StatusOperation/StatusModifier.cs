using Assets.Scripts.Interface;
using System;

namespace Assets.Scripts.Stats.StatsOperation {
    public class StatusModifier : IDisposable {
        public StatusType StatusType { get; }
        public IOperationStrategy Strategy { get; }
        public float Value { get; }
        public bool MarkedForRemoval { get; set; }

        public event Action<StatusModifier> OnDispose = delegate { };
        private CountdownTimer timer;

        public StatusModifier(StatusType type, IOperationStrategy strategy, float duration = 0) {
            StatusType = type;
            Strategy = strategy;

            if (duration <= 0) return;

            timer = new CountdownTimer(duration);
            timer.OnTimerStop += () => MarkedForRemoval = true;
            timer.Start();
        }

        public void Update(float deltaTime) => timer?.Tick(deltaTime);


        public void Dispose() => OnDispose.Invoke(this);
    }
}
