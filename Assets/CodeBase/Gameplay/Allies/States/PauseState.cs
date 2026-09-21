using CodeBase.Infrastructure.Common.StateMachine.Interfaces;

namespace CodeBase.Gameplay.Allies.States
{
    public class PauseState : IState
    {
        private readonly AllyView _allyView;

        public PauseState(AllyView allyView)
        {
            _allyView = allyView;
        }
        
        public void Enter()
        {
            // _allyView.AllyAnimator.Animator.enabled = false;
        }

        public void Exit()
        {
            // _allyView.AllyAnimator.Animator.enabled = true;
        }
    }
}