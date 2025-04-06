using Interface;
using UnityEngine;

public class WalkState : IPlayerState {
    private PlayerAnimator _playerAnimator;

    public WalkState(PlayerAnimator playerAnimator) {
        this._playerAnimator = playerAnimator;
    }

    public void Enter(ControllDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsMove, true, Direction, currentLookDirection);
    }

    public void Exit(ControllDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsMove, false, Direction, currentLookDirection);
    }

    public void Update(ControllDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsMove, true, Direction, currentLookDirection);
    }
}