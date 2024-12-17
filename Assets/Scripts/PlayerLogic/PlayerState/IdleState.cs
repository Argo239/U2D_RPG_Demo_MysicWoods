using Interface;
using UnityEngine;
using UnityEngine.EventSystems;

public class IdleState : IPlayerState {
    private PlayerAnimator _playerAnimator;

    public IdleState(PlayerAnimator playerAnimator) {
        this._playerAnimator = playerAnimator;
    }

    public void Enter(MoveDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsMoving, false, Direction, currentLookDirection);
    }

    public void Exit(MoveDirection Direction, Vector2 currentLookDirection) {

    }

    public void Update(MoveDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsMoving, false, Direction, currentLookDirection);
    }
}
