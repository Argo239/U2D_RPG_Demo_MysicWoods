using Interface;
using System.Collections.Generic;
using UnityEngine;
using static Argo_Utils.Utils;

namespace PlayerLogic {
    public class AnimatorStateMachine {
        private IPlayerState currentState;

        private readonly Dictionary<string, PlayerController.State> stateMapping = new Dictionary<string, PlayerController.State> {
            { "Idling", PlayerController.State.Idling },
            { "Moving", PlayerController.State.Moving },
            { "Running", PlayerController.State.Running },
            { "Attacking", PlayerController.State.Attacking },
            { "Dodge", PlayerController.State.Dodge },
            { "Dead", PlayerController.State.Dead }
        };

        public void ChangeState(IPlayerState newState, ControlDirection moveDirection, Vector2 currentLookDirection) {
            currentState?.Exit(moveDirection, currentLookDirection);
            currentState = newState;
            currentState.Enter(moveDirection, currentLookDirection);

            var stateName = newState.GetType().Name.Replace("State", "");
            if(stateMapping.TryGetValue(stateName, out var mappedState)) 
                PlayerController.Instance.SetCurrentState(mappedState);
        }

        public void Update(ControlDirection moveDirection, Vector2 currentLookDirection) {
            currentState?.Update(moveDirection, currentLookDirection);
            DebugLog(PlayerController.Instance.consoleLogOn, $"{currentState} is updating");
        }
    }
}