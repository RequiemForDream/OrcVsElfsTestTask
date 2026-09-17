using CodeBase.Gameplay.Common.Interfaces;
using CodeBase.Gameplay.TargetSystem.Interfaces;
using CodeBase.Infrastructure.Common.StateMachine.Interfaces;

namespace CodeBase.Gameplay.Enemies.States
{
    public class IdleState : IState
    {
        private readonly ITargetSelector _targetSelector;
        private readonly EnemyStateMachine _enemyStateMachine;
        private readonly EnemyView _enemyView;

        public IdleState(ITargetSelector targetSelector, EnemyStateMachine enemyStateMachine, EnemyView enemyView)
        {
            _enemyView = enemyView;
            _enemyStateMachine = enemyStateMachine;
            _targetSelector = targetSelector;
        }
        
        public void Enter()
        {
            _enemyView.EnemyAnimator.StopMoving();
            _targetSelector.OnTargetChanged += TargetFound;
            _targetSelector.SetTargetCollectionAllowed(true);
        }

        private void TargetFound(ITarget target)
        {
            _enemyStateMachine.Enter<MoveToTargetState, ITarget>(target);
        }

        public void Exit()
        {
            _targetSelector.SetTargetCollectionAllowed(false);
            _targetSelector.OnTargetChanged -= TargetFound;
        }
    }
}