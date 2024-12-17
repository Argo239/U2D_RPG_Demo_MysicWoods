using Interface;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveState : IPlayerState {
    private PlayerAnimator _playerAnimator;

    public MoveState(PlayerAnimator playerAnimator) {
        this._playerAnimator = playerAnimator;
    }

    public void Enter(MoveDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsMoving, true, Direction, currentLookDirection);
    }

    public void Exit(MoveDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsMoving, false, Direction, currentLookDirection);
    }

    public void Update(MoveDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsMoving, true, Direction, currentLookDirection);
    }
}