using UnityEngine;

public class DodgeState : ICompletableState {
    private readonly string DODGE = "Dodge";

    private PlayerAnimator _playerAnimator;

    public DodgeState(PlayerAnimator playerAnimator) {
        _playerAnimator = playerAnimator;
    }

    public void Enter(ControllDirection cardinalDir, Vector2 inputVector) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsDodge, true, cardinalDir, inputVector);
    }

    public void Exit(ControllDirection cardinalDir, Vector2 inputVector) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsDodge, false, cardinalDir, inputVector);
    }

    public void Update(ControllDirection cardinalDir, Vector2 inputVector) {
        if (_playerAnimator.CheckAnimationFinishedByTag(DODGE)) {
            Exit(cardinalDir, inputVector);
        }
    }

    public bool IsComplete(ControllDirection cardinalDir, Vector2 inputVector) {
        return PlayerAnimator.Instance.CheckAnimationFinishedByTag(DODGE);
    }
}