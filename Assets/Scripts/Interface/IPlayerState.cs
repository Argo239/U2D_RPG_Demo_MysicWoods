namespace Interface {
    public interface IPlayerState {
        void Enter(PlayerAnimator playerAnimator);
        void Execute(PlayerAnimator playerAnimator);
        void Exit(PlayerAnimator playerAnimator);
    }
}