using Interface;
using static PlayerAnimator;
using static Argo_Utils.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PlayerLogic {
    public class AnimatorStateMachine {
        private IPlayerState currentState;
        private PlayerAnimator playerAnimator;

        public void ChangeState(IPlayerState newState, MoveDirection moveDirection, Vector2 currentLookDirection) {
            currentState?.Exit(moveDirection, currentLookDirection);
            currentState = newState;
            currentState.Enter(moveDirection, currentLookDirection);
        }

        public void Update(MoveDirection moveDirection, Vector2 currentLookDirection) {
            currentState?.Update(moveDirection, currentLookDirection);
        }
    }
}
//public class IdleState : IPlayerState {
//    public void Enter(PlayerAnimator playerAnimator) => playerAnimator.TryToSetAnimation(IsMoving, false);
//    public void Update(PlayerAnimator playerAnimator) => playerAnimator.TryToSetAnimation(IsMoving, false);
//    public void Exit(PlayerAnimator playerAnimator) { }
//}

//public class MovingState : IPlayerState {
//    public void Enter(PlayerAnimator playerAnimator) => playerAnimator.TryToSetAnimation(IsMoving, true);
//    public void Update(PlayerAnimator playerAnimator) => playerAnimator.TryToSetAnimation(IsMoving, true);
//    public void Exit(PlayerAnimator playerAnimator) => playerAnimator.SetAnimatorBool(IsMoving, false);
//}

//}