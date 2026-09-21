using System;
using CodeBase.Common.Enums;
using CodeBase.Gameplay.Common.Health;
using CodeBase.Gameplay.Common.Interfaces;
using CodeBase.Gameplay.Currency;
using CodeBase.Gameplay.Enemies.States;
using CodeBase.Gameplay.Pause;
using CodeBase.Gameplay.TargetSystem;
using CodeBase.Gameplay.TargetSystem.Interfaces;
using CurvedPathGenerator;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Enemies
{
    public class Enemy :  IEnemy
    {
        public event Action<ITarget> OnDie;
        public event Action OnDestroyHandler;

        public TeamId Team => _enemyModel.TeamId;
        public Vector3 Position => _enemyView.transform.position;
        public Vector3 HitPosition => _enemyView.ModelCenter.position;
        public bool IsTargeted { get; set; }
        public int SpawnOrder { get; set; }

        public bool IsAlive { get; private set; } = true;
        public float Health => _health.CurrentHealth;

        private readonly EnemyView _enemyView;
        private readonly EnemyModel _enemyModel;
        private readonly PathGenerator _enemyWalkPath;
        private readonly ICurrencyCounter _currencyCounter;
        private readonly IPauseService _pauseService;

        private EnemyStateMachine _enemyStateMachine;

        private Health _health;

        private ITargetSelector _targetSelector;
        private readonly TickableManager _tickableManager;

        private Type _stateBeforePause;
        
        private bool _isPaused;

        public Enemy(EnemyView enemyView, EnemyModel enemyModel, PathGenerator enemyWalkPath,
            ICurrencyCounter currencyCounter, IPauseService pauseService, TickableManager tickableManager)
        {
            _tickableManager = tickableManager;
            _pauseService = pauseService;
            _currencyCounter = currencyCounter;
            _enemyWalkPath = enemyWalkPath;
            _enemyModel = enemyModel;
            _enemyView = enemyView;
        }

        public void Initialize()
        {
            _enemyView.OnDestroyHandler += Destroy;
            _enemyView.SetController(this);
            _tickableManager.Add(this);
            InitTargetingSystem();
            InitHealth();
            InitializeStateMachine();
            _enemyStateMachine.Enter<InitState>();
            SubscribeToPauseService();
        }
        
        private void SubscribeToPauseService()
        {
            _pauseService.AddListener(this);
            if (_pauseService.IsPaused)
            {
                OnPause();
            }
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
            _enemyStateMachine.AddState(typeof(PauseState), new PauseState(_enemyView));
            
        }
        
        private void InitTargetingSystem()
        {
            ITargetingFilter targetingFilter = new TargetFilter(_enemyModel.TeamId);
            ITargetsBuffer targetsBuffer = new TargetsBuffer(_enemyView.AttackRangeTrigger, targetingFilter);
            _targetSelector = new TargetSelector(targetsBuffer, _enemyView.transform, _enemyModel.TargetAttackType, true);
            _targetSelector.Initialize();
        }

        private void HandleHealthChanged(float currentHealth)
        {
            if (currentHealth <= 0)
            {
                if (_targetSelector.CurrentTarget != null)
                {
                    _targetSelector.CurrentTarget.IsTargeted = false;
                }
               
                IsAlive = false;
                OnDie?.Invoke(this);
                _enemyStateMachine.Enter<DeathState>();
                _currencyCounter.Add(_enemyModel.Reward);
            }
        }

        public void Tick()
        {
            if (_isPaused) return;
            
            _enemyStateMachine.UpdateStateLogic();
        }

        public void Destroy()
        {
            _pauseService.RemoveListener(this);
            _tickableManager.Remove(this);
            _health.OnHealthChanged -= HandleHealthChanged;
            _enemyView.OnDestroyHandler -= Destroy;
            _targetSelector.Dispose();
        }

        public void OnPause()
        {
            _isPaused = true;
            _stateBeforePause = _enemyStateMachine.ActiveStateType;
            _enemyStateMachine.Enter<PauseState>();
        }

        public void OnResume()
        {
            _enemyStateMachine.Enter(_stateBeforePause);
            _isPaused = false; 
            _stateBeforePause = null;
        }
    }
}