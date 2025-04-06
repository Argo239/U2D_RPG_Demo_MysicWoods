using Interface;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeadState : IPlayerState {
    private PlayerAnimator _playerAnimator;
    private Vector2 _deadDirection = new Vector2(-1f, 0f);

    public DeadState(PlayerAnimator playerAnimator) {
        this._playerAnimator = playerAnimator;
    }

    public void Enter(ControllDirection direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsDead, true, ControllDirection.Left, _deadDirection);
    }

    public void Exit(ControllDirection direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsDead, false, direction, _deadDirection);
    }

    public void Update(ControllDirection direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsDead, true, ControllDirection.Left, _deadDirection);
    }
}
