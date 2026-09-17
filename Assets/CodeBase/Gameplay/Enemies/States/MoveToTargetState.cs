using CodeBase.Gameplay.Common.Interfaces;
using CodeBase.Infrastructure.Common.StateMachine.Interfaces;
using UnityEngine;

namespace CodeBase.Gameplay.Enemies.States
{
    public class MoveToTargetState : IState, IUpdatableState, IPayLoadedState<ITarget>
    {
        private readonly EnemyStateMachine _enemyStateMachine;
        private readonly EnemyView _enemyView;
        private readonly EnemyModel _enemyModel;
        private ITarget _target;

        public MoveToTargetState(EnemyModel enemyModel, EnemyView enemyView, EnemyStateMachine enemyStateMachine)
        {
            _enemyStateMachine = enemyStateMachine;
            _enemyView = enemyView;
            _enemyModel = enemyModel;
        }

        public void Enter(ITarget target)
        {
            _target = target;
            _enemyView.EnemyAnimator.Move(_enemyModel.Speed);
        }

        public void Enter()
        {
            
        }

        public void UpdateLogic()
        {
            if (_target == null)
            {
                _enemyStateMachine.Enter<IdleState>();
                return;
            }

            Vector3 directionToTarget = (_target.Position - _enemyView.transform.position).normalized;
            float distanceToTarget = Vector3.Distance(_enemyView.transform.position, _target.Position);

            RotateTowardsTarget(directionToTarget);
            
            if (distanceToTarget <= _enemyModel.AttackRange)
            {
                _enemyStateMachine.Enter<AttackState, ITarget>(_target);
                return;
            }

            MoveTowardsTarget(directionToTarget);
        }

        public void Exit()
        {
            
        }

        private void MoveTowardsTarget(Vector3 direction)
        {
            Vector3 newPosition = _enemyView.transform.position + direction * _enemyModel.Speed * Time.deltaTime;
            _enemyView.transform.position = newPosition;
        }

        private void RotateTowardsTarget(Vector3 direction)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _enemyView.transform.rotation = Quaternion.Lerp(
                _enemyView.transform.rotation,
                targetRotation,
                _enemyModel.TurnSpeed * Time.deltaTime
            );
        }
    }
}