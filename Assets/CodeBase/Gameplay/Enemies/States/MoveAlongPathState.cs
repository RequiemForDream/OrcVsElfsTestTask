using CodeBase.Infrastructure.Common.StateMachine.Interfaces;
using CurvedPathGenerator;
using UnityEngine;

namespace CodeBase.Gameplay.Enemies.States
{
    public class MoveAlongPathState : IState, IUpdatableState
    {
        private readonly PathGenerator _enemyMarchPath;
        private readonly EnemyModel _enemyModel;
        private readonly EnemyView _enemyView;
        private readonly IStateMachine _enemyStateMachine;

        private int _pathIndex = 1;
        private Vector3 _nextPoint;

        public MoveAlongPathState(PathGenerator enemyMarchPath, EnemyModel enemyModel, EnemyView enemyView, IStateMachine enemyStateMachine)
        {
            _enemyStateMachine = enemyStateMachine;
            _enemyView = enemyView;
            _enemyModel = enemyModel;
            _enemyMarchPath = enemyMarchPath;
        }

        public void Enter()
        {
            _nextPoint = _enemyMarchPath.PathList[_pathIndex];
            _enemyView.EnemyAnimator.Move(_enemyModel.Speed);
        }

        public void UpdateLogic()
        {
            Move();
        }

        private void Move()
        {
            Transform transform = _enemyView.transform;

            Vector3 direction = (_nextPoint - transform.position).normalized;

            if (direction != Vector3.zero)
            {
                Quaternion targetRot = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRot,
                    _enemyModel.TurnSpeed * Time.deltaTime);
            }

            transform.position += direction * (_enemyModel.Speed * Time.deltaTime);

            float distance = Vector3.Distance(transform.position, _nextPoint);
            if (distance < _enemyModel.PathDistanceThreshold)
            {
                _pathIndex++;
                 
                if (_pathIndex >= _enemyMarchPath.PathList.Count)
                {
                    _enemyStateMachine.Enter<IdleState>();
                    return;
                }

                _nextPoint = _enemyMarchPath.PathList[_pathIndex];
            }
        }

        public void Exit()
        {
            
        }
    }
}