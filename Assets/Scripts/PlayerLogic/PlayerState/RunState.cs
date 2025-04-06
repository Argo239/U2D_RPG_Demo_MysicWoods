using Interface;
using UnityEngine;

public class RunState : IPlayerState {
    private PlayerAnimator _playerAnimator;

    public RunState(PlayerAnimator playerAnimator) {
        _playerAnimator = playerAnimator;
    }

    public void Enter(ControllDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsMove, true, Direction, currentLookDirection);
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsRun, true, Direction, currentLookDirection);
    }

    public void Exit(ControllDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsMove, false, Direction, currentLookDirection);
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsRun, false, Direction, currentLookDirection);
    }


    public void Update(ControllDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsMove, true, Direction, currentLookDirection);
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsRun, true, Direction, currentLookDirection);
    }
}
