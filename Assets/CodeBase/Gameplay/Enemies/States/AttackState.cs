using CodeBase.Gameplay.Common.Animations;
using CodeBase.Gameplay.Common.Interfaces;
using CodeBase.Infrastructure.Common.StateMachine.Interfaces;
using UnityEngine;
using EventType = CodeBase.Gameplay.Common.Animations.EventType;

namespace CodeBase.Gameplay.Enemies.States
{
    public class AttackState : IState, IUpdatableState, IPayLoadedState<ITarget>
    {
        private readonly EnemyStateMachine _enemyStateMachine;
        private readonly EnemyModel _enemyModel;
        private readonly EnemyView _enemyView;
        
        private ITarget _target;

        public AttackState(EnemyView enemyView, EnemyModel enemyModel, EnemyStateMachine enemyStateMachine)
        {
            _enemyView = enemyView;
            _enemyModel = enemyModel;
            _enemyStateMachine = enemyStateMachine;
        }

        public void Enter(ITarget target)
        {
            _target = target;
            _enemyView.EnemyAnimator.StopMoving();
            _enemyView.EnemyAnimator.PlayAttack();
            _enemyView.EnemyAnimator.StateExited += Attacked;
            _enemyView.EnemyAnimator.AnimationEventRelay.OnAnimationEventInvoke += ApplyDamageToTarget;
        }

        private void ApplyDamageToTarget(EventType eventType)
        {
            if (eventType == EventType.OnOrcAttack)
            {
                _target.ApplyDamage(_enemyModel.Damage);
            }
        }

        private void Attacked(AnimatorState exitedState)
        {
            if (exitedState == AnimatorState.Attack)
            {
                _enemyStateMachine.Enter<IdleState>();
            }
        }

        public void Enter()
        {
            
        }

        public void UpdateLogic()
        {
        }

        public void Exit()
        {
            _enemyView.EnemyAnimator.StateExited -= Attacked;
            _enemyView.EnemyAnimator.AnimationEventRelay.OnAnimationEventInvoke -= ApplyDamageToTarget;
        }
    }
}