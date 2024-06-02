using Interface;

namespace PlayerLogic {
    public class PlayerStateMachine {
        private IPlayerState _currentState;

        public void ChangeState(IPlayerState newState, PlayerAnimator playerAnimator) {
            _currentState?.Exit(playerAnimator);
            _currentState = newState;
            _currentState.Enter(playerAnimator);
        }

        public void Update(PlayerAnimator playerAnimator) {
            _currentState?.Execute(playerAnimator);
        }
    
    }

    public class IdleState : IPlayerState {
        public void Enter(PlayerAnimator playerAnimator) {
            playerAnimator.TryToSetAnimation(PlayerAnimator.IsMoving, false);
            playerAnimator.TryToSetAnimation(PlayerAnimator.IsAttacking, false);
        }

        public void Execute(PlayerAnimator playerAnimator) { }

        public void Exit(PlayerAnimator playerAnimator) { }
    }
    
    public class MovingState : IPlayerState {
        public void Enter(PlayerAnimator playerAnimator) {
            playerAnimator.UpdateMoveDirection();
            playerAnimator.TryToSetAnimation(PlayerAnimator.IsMoving, true);
        }

        public void Execute(PlayerAnimator playerAnimator) {
            
        }

        public void Exit(PlayerAnimator playerAnimator) {
            playerAnimator.SetAnimatorBool(PlayerAnimator.IsMoving, false);
        }
    }

    public class RunningState : IPlayerState {
        public void Enter(PlayerAnimator playerAnimator) {
            playerAnimator.UpdateMoveDirection();
            playerAnimator.Running();
            playerAnimator.TryToSetAnimation(PlayerAnimator.IsMoving, true);

        }

        public void Execute(PlayerAnimator playerAnimator) { }

        public void Exit(PlayerAnimator playerAnimator) {
            playerAnimator.SetAnimatorBool(PlayerAnimator.IsMoving, false);
        }
    }

    public class AttackingState : IPlayerState {
        public void Enter(PlayerAnimator playerAnimator) {
            playerAnimator.UpdateMoveDirection();
            playerAnimator.TryToSetAnimation(PlayerAnimator.IsAttacking, true);
            if(playerAnimator.GetAnimatorBool(PlayerAnimator.IsMoving)) 
                playerAnimator.SetAnimatorBool(PlayerAnimator.IsMoving, false);
        }

        public void Execute(PlayerAnimator playerAnimator) { }

        public void Exit(PlayerAnimator playerAnimator) {
            playerAnimator.SetAnimatorBool(PlayerAnimator.IsAttacking, false);
        }
    }
}