using UnityEngine;
using UnityEngine.EventSystems;

namespace Interface {
    public interface IPlayerState {
        void Enter(ControlDirection Direction, Vector2 currentLookDirection);
        void Exit(ControlDirection Direction, Vector2 currentLookDirection);
        void Update(ControlDirection Direction, Vector2 currentLookDirection);
    }
}