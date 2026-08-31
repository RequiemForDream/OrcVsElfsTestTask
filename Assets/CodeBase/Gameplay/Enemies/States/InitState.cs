using CodeBase.Infrastructure.Common.StateMachine.Interfaces;
using CurvedPathGenerator;

namespace CodeBase.Gameplay.Enemies.States
{
    public class InitState : IState
    {
        private readonly EnemyView _enemyView;
        private readonly PathGenerator _enemyMarchPath;
        private readonly IStateMachine _enemyStateMachine;

        public InitState(EnemyView enemyView, PathGenerator enemyMarchPath, IStateMachine enemyStateMachine)
        {
            _enemyView = enemyView;
            _enemyMarchPath = enemyMarchPath;
            _enemyStateMachine = enemyStateMachine;
        }
        
        public void Enter()
        {
            _enemyView.transform.position = _enemyMarchPath.PathList[0];       
            _enemyStateMachine.Enter<MarchingState>();
        }

        public void Exit()
        {
            
        }
    }
}