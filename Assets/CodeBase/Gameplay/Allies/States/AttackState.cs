using CodeBase.Gameplay.Arrows;
using CodeBase.Gameplay.Arrows.Factory;
using CodeBase.Gameplay.Common.Animations;
using CodeBase.Gameplay.Common.Interfaces;
using CodeBase.Gameplay.TargetSystem.Interfaces;
using CodeBase.Infrastructure.Common.StateMachine.Interfaces;
using UnityEngine;
using EventType = CodeBase.Gameplay.Common.Animations.EventType;


namespace CodeBase.Gameplay.Allies.States
{
    public class AttackState : IState, IUpdatableState
    {
        private readonly IArrowFactory _arrowFactory;
        private readonly AllyStateMachine _allyStateMachine;
        private readonly ITargetSelector _targetSelector;
        private readonly AllyView _allyView;
        private readonly AllyModel _allyModel;

        public AttackState(IArrowFactory arrowFactory, AllyStateMachine allyStateMachine, ITargetSelector targetSelector, AllyView allyView, AllyModel allyModel)
        {
            _allyView = allyView;
            _allyModel = allyModel;
            _arrowFactory = arrowFactory;
            _allyStateMachine = allyStateMachine;
            _targetSelector = targetSelector;
        }
        
        public void Enter()
        {
            _allyView.AllyAnimator.StateExited += OnAttackExit;
            _allyView.AllyAnimator.AnimationEventRelay.OnAnimationEventInvoke += SpawnArrow;
            _allyView.AllyAnimator.PlayAttack();
        }

        private void SpawnArrow(EventType eventType)
        {
            if (eventType == EventType.OnElfAttack)
            {
                ITarget target = _targetSelector.CurrentTarget;
                Arrow arrow = _arrowFactory.Create(_allyView.ArrowSpawnPoint.position, _allyModel.ArrowType);
                arrow.Release(target);
                arrow.OnHit += () =>
                {
                    target.ApplyDamage(_allyModel.Damage);
                };
            }
        }

        private void OnAttackExit(AnimatorState state)
        {
            if (state == AnimatorState.Attack)
            {
                if (_targetSelector.CurrentTarget != null)
                {
                    _allyView.AllyAnimator.PlayAttack();
                }
                else
                {
                    _allyStateMachine.Enter<IdleState>();
                }
            }
        }

        public void UpdateLogic()
        {
            if (_targetSelector.CurrentTarget == null) return;
            
            Vector3 direction = _targetSelector.CurrentTarget.Position - _allyView.transform.position;
            direction.y = 0f;
            _allyView.transform.rotation = Quaternion.LookRotation(direction);
        }

        public void Exit()
        {
            _allyView.AllyAnimator.AnimationEventRelay.OnAnimationEventInvoke -= SpawnArrow;
            _allyView.AllyAnimator.StateExited -= OnAttackExit;
        }
    }
}