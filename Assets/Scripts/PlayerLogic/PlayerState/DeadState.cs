using Interface;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeadState : IPlayerState {
    private PlayerAnimator _playerAnimator;
    private Vector2 _deadDirection = new Vector2(-1f, 0f);

    public DeadState(PlayerAnimator playerAnimator) {
        this._playerAnimator = playerAnimator;
    }

    public void Enter(MoveDirection direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsDead, true, MoveDirection.Left, _deadDirection);
    }

    public void Exit(MoveDirection direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsDead, false, direction, _deadDirection);
    }

    public void Update(MoveDirection direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsDead, true, MoveDirection.Left, _deadDirection);
    }
}
