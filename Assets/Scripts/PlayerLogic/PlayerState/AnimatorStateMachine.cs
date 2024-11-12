using Interface;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Argo_Utils.Utils;

namespace PlayerLogic {
    public class AnimatorStateMachine {
        private IPlayerState currentState;

        private readonly Dictionary<string, PlayerController.State> stateMapping = new Dictionary<string, PlayerController.State> {
            { "Idle", PlayerController.State.Idling },
            { "Move", PlayerController.State.Moving }
        };

        public void ChangeState(IPlayerState newState, MoveDirection moveDirection, Vector2 currentLookDirection) {
            currentState?.Exit(moveDirection, currentLookDirection);
            currentState = newState;
            currentState.Enter(moveDirection, currentLookDirection);

            var stateName = newState.GetType().Name.Replace("State", "");
            if(stateMapping.TryGetValue(stateName, out var mappedState)) 
                PlayerController.Instance.SetCurrentState(mappedState);
        }

        public void Update(MoveDirection moveDirection, Vector2 currentLookDirection) {
            currentState?.Update(moveDirection, currentLookDirection);

            LogMessage(PlayerController.Instance.consoleLogOn, $"{currentState} is updating");
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