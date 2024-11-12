using Interface;
using UnityEngine;
using UnityEngine.EventSystems;

public class IdleState : IPlayerState {
    private PlayerAnimator playerAnimator;


    public IdleState(PlayerAnimator playerAnimator) {
        this.playerAnimator = playerAnimator;
    }

    public void Enter(MoveDirection Direction, Vector2 currentLookDirection) {
        playerAnimator.TryToSetAnimation(PlayerAnimator.IsMoving, false, Direction, currentLookDirection);
    }

    public void Exit(MoveDirection Direction, Vector2 currentLookDirection) {

    }

    public void Update(MoveDirection Direction, Vector2 currentLookDirection) {
        playerAnimator.TryToSetAnimation(PlayerAnimator.IsMoving, false, Direction, currentLookDirection);
    }
}
