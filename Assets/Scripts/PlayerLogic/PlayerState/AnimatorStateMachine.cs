using Interface;
using System.Collections.Generic;
using UnityEngine;
using static Argo_Utils.Utils;

namespace PlayerLogic {
    public class AnimatorStateMachine {
        private IPlayerState currentState;

        public IPlayerState CurrentState => currentState;

        private readonly Dictionary<string, PlayerController.State> stateMapping = new Dictionary<string, PlayerController.State> {
            { "Idle", PlayerController.State.Idling },
            { "Walk", PlayerController.State.Moving },
            { "Run", PlayerController.State.Running },
            { "Attack", PlayerController.State.Attacking },
            { "Dodge", PlayerController.State.Dodge },
            { "Dead", PlayerController.State.Dead }
        };

        public void ChangeState(IPlayerState newState, ControllDirection moveDirection, Vector2 currentLookDirection) {
            if (currentState == null || currentState.GetType() != newState.GetType()) {
                currentState?.Exit(moveDirection, currentLookDirection);
                currentState = newState;
                currentState.Enter(moveDirection, currentLookDirection);

                var stateName = newState.GetType().Name.Replace("State", "");
                if (stateMapping.TryGetValue(stateName, out var mappedState))
                    PlayerController.Instance.SetCurrentState(mappedState);
            } else {
                currentState.Update(moveDirection, currentLookDirection);
            }
        }

        public void Update(ControllDirection moveDirection, Vector2 currentLookDirection) {
            currentState?.Update(moveDirection, currentLookDirection);
            DebugLog(PlayerController.Instance.consoleLogOn, $"{currentState} is updating");
        }
    }
}