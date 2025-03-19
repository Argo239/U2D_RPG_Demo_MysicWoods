using Interface;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveState : IPlayerState {
    private PlayerAnimator _playerAnimator;

    public MoveState(PlayerAnimator playerAnimator) {
        this._playerAnimator = playerAnimator;
    }

    public void Enter(ControlDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsMove, true, Direction, currentLookDirection);
    }

    public void Exit(ControlDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsMove, false, Direction, currentLookDirection);
    }

    public void Update(ControlDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsMove, true, Direction, currentLookDirection);
    }
}