using CodeBase.Infrastructure.Common.StateMachine.Interfaces;
using UnityEngine;

namespace CodeBase.Gameplay.Enemies.States
{
    public class ChasingState : IState, IUpdatableState
    {
        private readonly EnemyStateMachine _enemyStateMachine;
        private readonly EnemyView _enemyView;
        private readonly EnemyModel _enemyModel;

        public ChasingState( EnemyModel enemyModel, EnemyView enemyView, EnemyStateMachine enemyStateMachine)
        {
            _enemyStateMachine = enemyStateMachine;
            _enemyView = enemyView;
            _enemyModel = enemyModel;
        }
        
        public void Enter()
        {
            
        }

        public void UpdateLogic()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}