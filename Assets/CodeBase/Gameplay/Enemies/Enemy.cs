using System;
using CodeBase.Common.Enums;
using CodeBase.Gameplay.Common.Health;
using CodeBase.Gameplay.Common.Interfaces;
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
        private EnemyStateMachine _enemyStateMachine;

        private Health _health;
        
        private ITargetsBuffer _targetsBuffer;
        private ITargetSelector _targetSelector;

        public Enemy(EnemyView enemyView, EnemyModel enemyModel, PathGenerator enemyWalkPath)
        {
            _enemyWalkPath = enemyWalkPath;
            _enemyModel = enemyModel;
            _enemyView = enemyView;
        }

        public void Initialize()
        {
            _enemyView.OnDestroyHandler += Destroy;
            _enemyView.SetController(this);
            InitializeStateMachine();
          //  InitTargetingSystem();
            InitHealth();
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
            _enemyStateMachine.AddState(typeof(MarchingState),
                new MarchingState(_enemyWalkPath, _enemyModel, _enemyView, _enemyStateMachine));
            _enemyStateMachine.AddState(typeof(ChasingState),
                new ChasingState(_enemyModel, _enemyView, _enemyStateMachine));
            _enemyStateMachine.AddState(typeof(DeathState), new DeathState(_enemyView, _enemyModel));
            _enemyStateMachine.Enter<InitState>();
        }
        
        private void InitTargetingSystem()
        {
            ITargetingFilter targetingFilter = new TargetFilter(_enemyModel.TeamId);
            _targetsBuffer = new TargetsBuffer(_enemyView.AttackRangeTrigger, targetingFilter);
            _targetSelector = new TargetSelector(_targetsBuffer, _enemyView.transform, _enemyModel.TargetAttackType);
            _targetsBuffer.Initialize();
            _targetSelector.Initialize();
        }

        private void HandleHealthChanged(float currentHealth)
        {
            if (currentHealth <= 0)
            {
                IsAlive = false;
                OnDie?.Invoke(this);
                _enemyStateMachine.Enter<DeathState>();
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
        }
    }
}