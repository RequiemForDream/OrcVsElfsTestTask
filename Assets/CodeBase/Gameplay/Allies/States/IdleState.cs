using CodeBase.Gameplay.TargetSystem.Interfaces;
using CodeBase.Infrastructure.Common.StateMachine.Interfaces;

namespace CodeBase.Gameplay.Allies.States
{
    public class IdleState : IState
    {
        private readonly AllyView _allyView;
        private readonly AllyModel _allyModel;
        private readonly AllyStateMachine _allyStateMachine;
        private readonly ITargetSelector _targetSelector;

        public IdleState(AllyView allyView, AllyModel allyModel, AllyStateMachine allyStateMachine, ITargetSelector targetSelector)
        {
            _targetSelector = targetSelector;
            _allyStateMachine = allyStateMachine;
            _allyView = allyView;
            _allyModel = allyModel;
        }

        public void Enter()
        {
            _targetSelector.OnTargetChanged += Attack;
        }

        private void Attack()
        {
             _allyStateMachine.Enter<AttackState>();
        }

        public void Exit()
        {
            _targetSelector.OnTargetChanged -= Attack;
        }
    }
}