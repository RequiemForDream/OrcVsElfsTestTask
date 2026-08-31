using CodeBase.Infrastructure.Common.StateMachine.Interfaces;

namespace CodeBase.Gameplay.Allies.States
{
    public class DeathState : IState
    {
        private readonly AllyView _allyView;

        public DeathState(AllyView allyView)
        {
            _allyView = allyView;
        }

        public void Enter()
        {
            _allyView.AllyAnimator.PlayDeath();
            _allyView.Collider.enabled = false;
        }

        public void Exit()
        {
            
        }
    }
}