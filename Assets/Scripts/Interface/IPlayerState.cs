namespace Interface {
    public interface IPlayerState {
        void Enter(PlayerAnimator playerAnimator);
        void Exit(PlayerAnimator playerAnimator);
        void Update(PlayerAnimator playerAnimator);
    }
}