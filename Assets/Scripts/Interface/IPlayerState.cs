using UnityEngine;
using UnityEngine.EventSystems;

namespace Interface {
    public interface IPlayerState {
        void Enter(ControllDirection Direction, Vector2 currentLookDirection);
        void Exit(ControllDirection Direction, Vector2 currentLookDirection);
        void Update(ControllDirection Direction, Vector2 currentLookDirection);
    }
}