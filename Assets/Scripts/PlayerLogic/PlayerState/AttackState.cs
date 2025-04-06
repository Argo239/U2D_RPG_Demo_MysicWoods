using Interface;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackState : IPlayerState {
    private PlayerAnimator _playerAnimator;
    private int count = 0;

    public AttackState(PlayerAnimator playerAnimator) {
        this._playerAnimator = playerAnimator;
    }

    public void Enter(ControllDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.SetAnimatorInt(PlayerAnimator.AttackCount, count);
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsAttack, true, Direction, currentLookDirection);

    }

    public void Exit(ControllDirection Direction, Vector2 currentLookDirection) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsAttack, false, Direction, currentLookDirection);
    }

    public void Update(ControllDirection Direction, Vector2 currentLookDirection) {
        if (_playerAnimator.IsAttackAnimationFinished()) {
            Exit(Direction, currentLookDirection);
        }
    }
}