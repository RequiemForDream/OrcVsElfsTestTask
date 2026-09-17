using System;
using CodeBase.Common.Enums;
using CodeBase.Gameplay.Common.Health;
using CodeBase.Gameplay.Common.Interfaces;
using CodeBase.Gameplay.Currency;
using CodeBase.Gameplay.Enemies.States;
using CodeBase.Gameplay.TargetSystem;
using CodeBase.Gameplay.TargetSystem.Interfaces;
using CurvedPathGenerator;
using UnityEngine;

namespace CodeBase.Gameplay.Enemies
{
    public class Enemy :  IEnemy
    {
        public event Action<ITarget> OnDie;
        public event Action OnDestroy;

        public TeamId Team => _enemyModel.TeamId;
        public Vector3 Position => _enemyView.transform.position;
        public Vector3 HitPosition => _enemyView.ModelCenter.position;

        public bool IsAlive { get; private set; } = true;
        
        private readonly EnemyView _enemyView;
        private readonly EnemyModel _enemyModel;
        private readonly PathGenerator _enemyWalkPath;
        private readonly ICurrencyCounter _currencyCounter;
        
        private EnemyStateMachine _enemyStateMachine;

        private Health _health;

        private ITargetSelector _targetSelector;

        public Enemy(EnemyView enemyView, EnemyModel enemyModel, PathGenerator enemyWalkPath,
            ICurrencyCounter currencyCounter)
        {
            _currencyCounter = currencyCounter;
            _enemyWalkPath = enemyWalkPath;
            _enemyModel = enemyModel;
            _enemyView = enemyView;
        }

        public void Initialize()
        {
            _enemyView.OnDestroyHandler += Destroy;
            _enemyView.SetController(this);
            InitTargetingSystem();
            InitHealth();
            InitializeStateMachine();
        }

        public void ApplyDamage(float damage)
        {
            _health.ApplyDamage(damage);
        }

        private void InitHealth()
        {
            _health = new Health(_enemyModel.Health, _enemyView.HealthBar);
            _health.OnHealthChanged += HandleHealthChanged;
        }

        private void InitializeStateMachine()
        {
            _enemyStateMachine = new EnemyStateMachine();
            _enemyStateMachine.AddState(typeof(InitState),
                new InitState(_enemyView, _enemyWalkPath, _enemyStateMachine));
            _enemyStateMachine.AddState(typeof(IdleState), new IdleState(_targetSelector, _enemyStateMachine, _enemyView));
            _enemyStateMachine.AddState(typeof(MoveAlongPathState),
                new MoveAlongPathState(_enemyWalkPath, _enemyModel, _enemyView, _enemyStateMachine));
            _enemyStateMachine.AddState(typeof(MoveToTargetState),
                new MoveToTargetState(_enemyModel, _enemyView, _enemyStateMachine));
            _enemyStateMachine.AddState(typeof(DeathState), new DeathState(_enemyView, _enemyModel));
            _enemyStateMachine.AddState(typeof(AttackState), new AttackState(_enemyView, _enemyModel, _enemyStateMachine));
            _enemyStateMachine.AddState(typeof(MoveToTargetState), new MoveToTargetState(_enemyModel, _enemyView, _enemyStateMachine));
            _enemyStateMachine.Enter<InitState>();
        }
        
        private void InitTargetingSystem()
        {
            ITargetingFilter targetingFilter = new TargetFilter(_enemyModel.TeamId);
            ITargetsBuffer targetsBuffer = new TargetsBuffer(_enemyView.AttackRangeTrigger, targetingFilter);
            _targetSelector = new TargetSelector(targetsBuffer, _enemyView.transform, _enemyModel.TargetAttackType);
            _targetSelector.Initialize();
        }

        private void HandleHealthChanged(float currentHealth)
        {
            if (currentHealth <= 0)
            {
                IsAlive = false;
                OnDie?.Invoke(this);
                _enemyStateMachine.Enter<DeathState>();
                _currencyCounter.Add(_enemyModel.Reward);
            }
        }

        public void Tick()
        {
            _enemyStateMachine.UpdateStateLogic();
        }

        public void Destroy()
        {
            _health.OnHealthChanged -= HandleHealthChanged;
            _enemyView.OnDestroyHandler -= Destroy;
            _targetSelector.Dispose();
        }

        public bool IsActiveInHierarchy => _enemyView.gameObject.activeInHierarchy;

        public void SetActive(bool value)
        {
            _enemyView.gameObject.SetActive(value);
        }
    }
}