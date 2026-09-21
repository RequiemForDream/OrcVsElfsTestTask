using CodeBase.Infrastructure.Common.StateMachine.Interfaces;

namespace CodeBase.Gameplay.Allies.States
{
    public class VictoryState : IState
    {
        private readonly AllyView _allyView;

        public VictoryState(AllyView allyView)
        {
            _allyView = allyView;
        }
        
        public void Enter()
        {
            _allyView.AllyAnimator.PlayVictory();
        }

        public void Exit()
        {
            
        }
    }
}