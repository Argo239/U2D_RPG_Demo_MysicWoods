using UnityEngine;

/// <summary>
/// 
/// </summary>
public class AttackState : ICompletableState {
    private readonly PlayerAnimator _playerAnimator;
    private readonly PlayerAttack _playerAttack;
    private readonly ComboStepData _stepData;

    private Vector2 fixedAttackDirection = Vector2.zero;

    public AttackState(PlayerAnimator playerAnimator, PlayerAttack playerAttack, ComboStepData stepData) {
        _playerAnimator = playerAnimator;
        _playerAttack = playerAttack;
        _stepData = stepData;
    }

    public void Enter(ControllDirection cardinalDir, Vector2 inputVector) {
        _playerAnimator.SetAnimatorInt(PlayerAnimator.AttackCount, _stepData.ComboSegment);
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsAttack, true, cardinalDir, fixedAttackDirection);
    }

    public void Exit(ControllDirection cardinalDir, Vector2 inputVector) {
        _playerAnimator.TryToSetAnimation(PlayerAnimator.IsAttack, false, cardinalDir, fixedAttackDirection);
    }

    public void Update(ControllDirection cardinalDir, Vector2 inputVector) {

    }

    public bool IsComplete(ControllDirection cardinalDir, Vector2 inputVector) {
        return _playerAnimator.CheckAnimationFinishedByName(_stepData.AnimationName);
    }
}
