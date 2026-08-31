using CodeBase.Gameplay.Arrows;
using CodeBase.Gameplay.Arrows.Factory;
using CodeBase.Gameplay.Common;
using CodeBase.Gameplay.Common.Animations;
using CodeBase.Gameplay.Common.Interfaces;
using CodeBase.Gameplay.TargetSystem;
using CodeBase.Gameplay.TargetSystem.Interfaces;
using CodeBase.Infrastructure.Common.StateMachine.Interfaces;
using Cysharp.Threading.Tasks;
using UnityEngine;

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
            Attack();
        }

        private void Attack()
        {
            _allyView.AllyAnimator.PlayAttack();
            ITarget target = _targetSelector.CurrentTarget;
            Arrow arrow = _arrowFactory.Create(_allyView.transform.position, _allyModel.ArrowType);
            arrow.Release(target);
            arrow.OnHit += () =>
            {
                target.ApplyDamage(_allyModel.Damage);
            };
        }

        private async void OnAttackExit(AnimatorState state)
        {
            if (state == AnimatorState.Attack)
            {
                await UniTask.Delay(300);
                if (_targetSelector.CurrentTarget != null)
                {
                    Attack();
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
            _allyView.AllyAnimator.StateExited -= OnAttackExit;
        }
    }
}