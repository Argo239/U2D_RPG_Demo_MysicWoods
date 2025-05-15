using Interface;
using UnityEngine;
using UnityEngine.EventSystems;

public class IdleState : IPlayerState {
    private PlayerAnimator _playerAnimator;

    public IdleState(PlayerAnimator playerAnimator) {
        this._playerAnimator = playerAnimator;
    }

    public void Enter(ControllDirection cardinalDir, Vector2 inputVector) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsMove, false, cardinalDir, inputVector);
    }

    public void Exit(ControllDirection cardinalDir, Vector2 inputVector) {

    }

    public void Update(ControllDirection cardinalDir, Vector2 inputVector) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsMove, false, cardinalDir, inputVector);
    }
}
