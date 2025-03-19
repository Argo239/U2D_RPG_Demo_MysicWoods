using Interface;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeadState : IPlayerState {
    private PlayerAnimator _playerAnimator;
    private Vector2 _deadDirection = new Vector2(-1f, 0f);

    public DeadState(PlayerAnimator playerAnimator) {
        this._playerAnimator = playerAnimator;
    }

    public void Enter(ControlDirection direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsDead, true, ControlDirection.Left, _deadDirection);
    }

    public void Exit(ControlDirection direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsDead, false, direction, _deadDirection);
    }

    public void Update(ControlDirection direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsDead, true, ControlDirection.Left, _deadDirection);
    }
}
