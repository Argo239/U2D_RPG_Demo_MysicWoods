using Interface;
using UnityEngine;

public class DodgeState : IPlayerState {
    private PlayerAnimator _playerAnimator;

    public DodgeState(PlayerAnimator playerAnimator) {
        _playerAnimator = playerAnimator;
    }

    public void Enter(ControllDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsAttack, true, Direction, currentLookDirection);
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsSprintDodge, true, Direction, currentLookDirection);
    }

    public void Exit(ControllDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsAttack, false, Direction, currentLookDirection);
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsSprintDodge, false, Direction, currentLookDirection);
    }

    public void Update(ControllDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsAttack, true, Direction, currentLookDirection);
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsSprintDodge, true, Direction, currentLookDirection);
    }
}