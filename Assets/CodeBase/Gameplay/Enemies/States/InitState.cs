using CodeBase.Infrastructure.Common.StateMachine.Interfaces;
using CurvedPathGenerator;

namespace CodeBase.Gameplay.Enemies.States
{
    public class InitState : IState
    {
        private readonly EnemyView _enemyView;
        private readonly PathGenerator _enemyMarchPath;
        private readonly IStateMachine _enemyStateMachine;
        private bool _isInitialized;

        public InitState(EnemyView enemyView, PathGenerator enemyMarchPath, IStateMachine enemyStateMachine)
        {
            _enemyView = enemyView;
            _enemyMarchPath = enemyMarchPath;
            _enemyStateMachine = enemyStateMachine;
        }
        
        public void Enter()
        {
            if (_isInitialized)
            {
                _enemyStateMachine.Enter<MoveAlongPathState>();
                return;
            }

            _isInitialized = true;
            _enemyView.transform.position = _enemyMarchPath.PathList[0];       
            _enemyStateMachine.Enter<MoveAlongPathState>();
        }

        public void Exit()
        {
            
        }
    }
}