using UnityEngine;

namespace Interface {
    public interface IPlayerState {
        void Enter(ControllDirection cardinalDir, Vector2 inputVector);
        void Exit(ControllDirection cardinalDir, Vector2 inputVector);
        void Update(ControllDirection cardinalDir, Vector2 inputVector);
    }
}