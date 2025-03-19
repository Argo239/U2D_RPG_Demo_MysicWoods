using Interface;
using UnityEngine;

namespace Assets.Scripts.PlayerLogic.PlayerState {
    public class DodgeState : IPlayerState {
        private PlayerAnimator _playerAnimator;

        public DodgeState(PlayerAnimator playerAnimator) {
            _playerAnimator = playerAnimator;
        }

        public void Enter(ControlDirection Direction, Vector2 currentLookDirection) {
            _playerAnimator.TryToSetAnimation(PlayerAnimator.IsSprintDodge, true, Direction, currentLookDirection);
        }

        public void Exit(ControlDirection Direction, Vector2 currentLookDirection) {
            _playerAnimator.TryToSetAnimation(PlayerAnimator.IsSprintDodge, false, Direction, currentLookDirection);
        }

        public void Update(ControlDirection Direction, Vector2 currentLookDirection) {
            _playerAnimator.TryToSetAnimation(PlayerAnimator.IsSprintDodge, true, Direction, currentLookDirection);
        }
    }
}
