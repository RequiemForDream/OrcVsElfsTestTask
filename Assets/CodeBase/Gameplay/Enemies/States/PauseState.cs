using CodeBase.Infrastructure.Common.StateMachine.Interfaces;

namespace CodeBase.Gameplay.Enemies.States
{
    public class PauseState : IState
    {
        private readonly EnemyView _enemyView;

        public PauseState(EnemyView enemyView)
        {
            _enemyView = enemyView;
        }
        
        public void Enter()
        {
             _enemyView.EnemyAnimator.Animator.enabled = false;
        }

        public void Exit()
        {
             _enemyView.EnemyAnimator.Animator.enabled = true;
        }
    }
}