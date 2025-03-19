using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.PlayerLogic.PlayerState {
    public class RunState : IPlayerState {
        private PlayerAnimator _playerAnimator;

        public RunState(PlayerAnimator playerAnimator) {
            _playerAnimator = playerAnimator;
        }

        public void Enter(ControlDirection Direction, Vector2 currentLookDirection) {
            _playerAnimator.TryToSetAnimation(PlayerAnimator.IsSprintDodge, true, Direction, currentLookDirection);
        }

        public void Exit(ControlDirection Direction, Vector2 currentLookDirection) {
            _playerAnimator.TryToSetAnimation(PlayerAnimator.IsSprintDodge, false, Direction, currentLookDirection);
        }

        public void Update(ControlDirection Direction, Vector2 currentLookDirection) {
            _playerAnimator.TryToSetAnimation(PlayerAnimator.IsSprintDodge, true, Direction, currentLookDirection);

        }
    }
