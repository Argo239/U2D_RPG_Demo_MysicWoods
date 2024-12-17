using Interface;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeadState : IPlayerState {
    private PlayerAnimator _playerAnimator;
    private Vector2 _deadDirection = Vector2.right;

    public DeadState(PlayerAnimator playerAnimator) {
        this._playerAnimator = playerAnimator;
    }

    public void Enter(MoveDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsMoving, false, Direction, currentLookDirection);
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsDead, true, Direction, _deadDirection);
    }

    public void Exit(MoveDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsDead, false, Direction, _deadDirection);
    }

    public void Update(MoveDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsDead, true, Direction, _deadDirection);
    }
}
