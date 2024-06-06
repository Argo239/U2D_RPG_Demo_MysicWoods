using Interface;
using static PlayerAnimator;
using static Argo_Utils.Utils;
using UnityEngine;

namespace PlayerLogic {
    public class PlayerStateMachine {
        private IPlayerState _currentState;

        public IPlayerState CurrentState => _currentState;

        public void ChangeState(IPlayerState newState, PlayerAnimator playerAnimator) {
            _currentState?.Exit(playerAnimator);
            _currentState = newState;
            _currentState.Enter(playerAnimator);
        }

        public void Update(PlayerAnimator playerAnimator) {
            _currentState?.Update(playerAnimator);
        }
    
    }

    public class IdleState : IPlayerState {
        public void Enter(PlayerAnimator playerAnimator) => playerAnimator.TryToSetAnimation(IsMoving, false);
        public void Update(PlayerAnimator playerAnimator) { }
        public void Exit(PlayerAnimator playerAnimator) { }
    }

    public class MovingState : IPlayerState {
        public void Enter(PlayerAnimator playerAnimator) => playerAnimator.TryToSetAnimation(IsMoving, true);
        public void Update(PlayerAnimator playerAnimator) => playerAnimator.TryToSetAnimation(IsMoving, true);
        public void Exit(PlayerAnimator playerAnimator) => playerAnimator.SetAnimatorBool(IsMoving, false);
    }

    public class RunningState : IPlayerState {
        PlayerStateMachine _stateMachine = new PlayerStateMachine();

        public void Enter(PlayerAnimator playerAnimator) {
            playerAnimator.Running();
            playerAnimator.TryToSetAnimation(IsMoving, true);
        }
        public void Update(PlayerAnimator playerAnimator) {
            if (!PlayerMove.Instance.GetRunningState()) {
                if (GameInput.Instance.GetMovementVectorNormalized() == Vector2.zero)
                    playerAnimator.GetStateMachine().ChangeState(new IdleState(), playerAnimator);
                else playerAnimator.GetStateMachine().ChangeState(new MovingState(), playerAnimator);
            } else {
                playerAnimator.Running();
                playerAnimator.TryToSetAnimation(IsMoving, true);
            }
        }
        public void Exit(PlayerAnimator playerAnimator) => playerAnimator.TryToSetAnimation(IsMoving, false);
    }

    public class AttackingState : IPlayerState {
        public void Enter(PlayerAnimator playerAnimator) {
            playerAnimator.TryToSetAnimation(IsAttacking, true);
            if (playerAnimator.GetAnimatorBool(IsMoving))
                playerAnimator.SetAnimatorBool(IsMoving, false);
        }
        public void Update(PlayerAnimator playerAnimator) => playerAnimator.TryToSetAnimation(IsAttacking, true);
        public void Exit(PlayerAnimator playerAnimator) => playerAnimator.SetAnimatorBool(IsAttacking, false);
    }
}