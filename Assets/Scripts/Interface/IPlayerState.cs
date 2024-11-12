using UnityEngine;
using UnityEngine.EventSystems;

namespace Interface {
    public interface IPlayerState {
        void Enter(MoveDirection Direction, Vector2 currentLookDirection);
        void Exit(MoveDirection Direction, Vector2 currentLookDirection);
        void Update(MoveDirection Direction, Vector2 currentLookDirection);
    }
}