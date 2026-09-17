using System;
using CodeBase.Common.Enums;
using CodeBase.Gameplay.Allies.States;
using CodeBase.Gameplay.Arrows.Factory;
using CodeBase.Gameplay.Common.Health;
using CodeBase.Gameplay.Common.Interfaces;
using CodeBase.Gameplay.Enemies;
using CodeBase.Gameplay.TargetSystem;
using CodeBase.Gameplay.TargetSystem.Interfaces;
using CodeBase.Gameplay.Tiles;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Allies
{
    public class Ally : IAlly
    {
        public event Action OnDestroy;
        public event Action<ITarget> OnDie;
        public ITile Tile { get; private set; }

        public bool IsAlive { get; private set; } = true;
        public TeamId Team => _allyModel.TeamId;
        public Vector3 Position => _allyView.transform.position;
        public Vector3 HitPosition => _allyView.transform.position;

        private readonly AllyView _allyView;
        private readonly AllyModel _allyModel;
        private readonly TickableManager _tickableManager;
        private readonly IArrowFactory _arrowFactory;

        private AllyStateMachine _allyStateMachine;
        private Health _health;
        
        private ITargetSelector _targetSelector;
        
        public Ally(AllyView allyView, AllyModel allyModel, TickableManager tickableManager, IArrowFactory arrowFactory)
        {
            _arrowFactory = arrowFactory;
            _allyView = allyView;
            _allyModel = allyModel;
            _tickableManager = tickableManager;
        }

        public void Initialize()
        {
            _tickableManager.Add(this);
            _allyView.OnDestroyHandler += Destroy;
            _allyView.SetTarget(this);
            _health = new Health(_allyModel.Health);
            _health.OnHealthChanged += HandleHealthChanged;
            InitTargetingSystem();
            InitializeStateMachine();
        }

        private void InitTargetingSystem()
        {
            ITargetingFilter targetingFilter = new TargetFilter(_allyModel.TeamId);
            TargetsBuffer targetsBuffer = new TargetsBuffer(_allyView.AttackRangeTrigger, targetingFilter);
            _targetSelector = new TargetSelector(targetsBuffer, _allyView.transform, _allyModel.TargetAttackType);
            _targetSelector.Initialize();
        }

        private void InitializeStateMachine()
        {
            _allyStateMachine = new AllyStateMachine();
            _allyStateMachine.AddState(typeof(InitState), new InitState());
            _allyStateMachine.AddState(typeof(IdleState), new IdleState(_allyView, _allyModel, _allyStateMachine, _targetSelector));
            _allyStateMachine.AddState(typeof(AttackState), new AttackState(_arrowFactory, _allyStateMachine, _targetSelector, _allyView, _allyModel));
            _allyStateMachine.AddState(typeof(DeathState), new DeathState(_allyView));
            _allyStateMachine.Enter<IdleState>();
        }

        public void Tick()
        {
            _allyStateMachine.UpdateStateLogic();
        }

        public void SetTile(ITile tile) => Tile = tile;

        public void ApplyDamage(float damage)
        {
            _health.ApplyDamage(damage);
        }
        
        private void HandleHealthChanged(float currentHealth)
        {
            if (currentHealth <= 0)
            {
                IsAlive = false;
                OnDie?.Invoke(this);
                _allyStateMachine.Enter<DeathState>();
            }
        }

        public void Destroy()
        {
            _tickableManager.Remove(this);
            _allyView.OnDestroyHandler -= Destroy;
            _health.OnHealthChanged -= HandleHealthChanged;
            _targetSelector.Dispose();
        }
    }
}