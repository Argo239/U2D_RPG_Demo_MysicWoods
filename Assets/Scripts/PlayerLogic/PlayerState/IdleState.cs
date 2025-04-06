using Interface;
using UnityEngine;
using UnityEngine.EventSystems;

public class IdleState : IPlayerState {
    private PlayerAnimator _playerAnimator;

    public IdleState(PlayerAnimator playerAnimator) {
        this._playerAnimator = playerAnimator;
    }

    public void Enter(ControllDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsMove, false, Direction, currentLookDirection);
    }

    public void Exit(ControllDirection Direction, Vector2 currentLookDirection) {

    }

    public void Update(ControllDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsMove, false, Direction, currentLookDirection);
    }
}
