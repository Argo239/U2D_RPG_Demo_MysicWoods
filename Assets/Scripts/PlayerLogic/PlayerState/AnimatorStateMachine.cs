using Interface;
using System.Collections.Generic;
using UnityEngine;
using static Argo_Utils.Utils;

namespace PlayerLogic {
    public class AnimatorStateMachine {

        private PlayerAnimator playerAnimator;
        private IPlayerState currentState;
        public IPlayerState CurrentState => currentState;

        private readonly Dictionary<string, PlayerController.State> stateMapping = new Dictionary<string, PlayerController.State> {
            { "Idle", PlayerController.State.Idling },
            { "Walk", PlayerController.State.Moving },
            { "Run", PlayerController.State.Running },
            { "AttackStep", PlayerController.State.Attacking },
            { "Dodge", PlayerController.State.Dodge },
            { "Dead", PlayerController.State.Dead }
        };

        public AnimatorStateMachine(PlayerAnimator playerAnimator) {
            this.playerAnimator = playerAnimator;
            currentState = new IdleState(this.playerAnimator);
        }

        public void Start() {
            PlayerController.Instance.SetCurrentState(PlayerController.State.Idling);
        }

        public void Update(ControllDirection cardinalDir, Vector2 inputVector) {
            currentState?.Update(cardinalDir, inputVector);
            if (currentState is ICompletableState completable && completable.IsComplete(cardinalDir, inputVector)) {
                ChangeState(new IdleState(playerAnimator), cardinalDir, inputVector, forceTransition: true);
                return;
            }

            DebugLog(PlayerController.Instance.consoleLogOn, $"{currentState} is updating");
        }

        public void ChangeState(IPlayerState newState, ControllDirection cardinalDir, Vector2 inputVector, bool forceTransition = false) {
            if (currentState == null || currentState.GetType() != newState.GetType() || forceTransition) {
                currentState?.Exit(cardinalDir, inputVector);
                currentState = newState;
                currentState.Enter(cardinalDir, inputVector);

                var stateName = newState.GetType().Name.Replace("State", "");
                if (stateMapping.TryGetValue(stateName, out var mappedState))
                    PlayerController.Instance.SetCurrentState(mappedState);
            } else {
                currentState.Update(cardinalDir, inputVector);
            }
        }
    }
}