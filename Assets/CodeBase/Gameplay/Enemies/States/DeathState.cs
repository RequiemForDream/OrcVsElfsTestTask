using CodeBase.Infrastructure.Common.StateMachine.Interfaces;

namespace CodeBase.Gameplay.Enemies.States
{
    public class DeathState : IState
    {
        private readonly EnemyView _enemyView;
        private readonly EnemyModel _enemyModel;

        public DeathState(EnemyView enemyView, EnemyModel enemyModel)
        {
            _enemyView = enemyView;
            _enemyModel = enemyModel;
        }

        public void Enter()
        {
            _enemyView.Collider.enabled = false;
            _enemyView.HealthBar.gameObject.SetActive(false);
            _enemyView.EnemyAnimator.PlayDeath();
        }

        public void Exit()
        {
            
        }
    }
}