using Interface;
using UnityEngine;
using UnityEngine.EventSystems;

public class IdleState : IPlayerState {
    private PlayerAnimator _playerAnimator;

    public IdleState(PlayerAnimator playerAnimator) {
        this._playerAnimator = playerAnimator;
    }

    public void Enter(ControlDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsMove, false, Direction, currentLookDirection);
    }

    public void Exit(ControlDirection Direction, Vector2 currentLookDirection) {

    }

    public void Update(ControlDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsMove, false, Direction, currentLookDirection);
    }
}
