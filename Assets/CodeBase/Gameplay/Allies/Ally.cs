using System;
using CodeBase.Common.Enums;
using CodeBase.Gameplay.Allies.States;
using CodeBase.Gameplay.Arrows.Factory;
using CodeBase.Gameplay.Common.Health;
using CodeBase.Gameplay.Common.Interfaces;
using CodeBase.Gameplay.Enemies;
using CodeBase.Gameplay.EnemySpawn;
using CodeBase.Gameplay.Levels;
using CodeBase.Gameplay.Merge;
using CodeBase.Gameplay.Pause;
using CodeBase.Gameplay.TargetSystem;
using CodeBase.Gameplay.TargetSystem.Interfaces;
using CodeBase.Gameplay.Tiles;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace CodeBase.Gameplay.Allies
{
    public class Ally : IAlly
    {
        public event Action OnDestroyHandler;
        public event Action<ITarget> OnDie;
        public ITile Tile { get; private set; }
        public AllyType AllyType => _allyModel.AllyType;

        public bool IsAlive { get; private set; } = true;
        public float Health => _health.CurrentHealth;
        public TeamId Team => _allyModel.TeamId;
        public Vector3 Position => _allyView.transform.position;
        public Vector3 HitPosition => _allyView.transform.position;
        public bool IsTargeted { get; set; }
        public int SpawnOrder { get; set; }
        public Transform Transform => _allyView.transform;

        private readonly AllyView _allyView;
        private readonly AllyModel _allyModel;
        private readonly TickableManager _tickableManager;
        private readonly IArrowFactory _arrowFactory;
        private readonly ILevelDataProvider _levelDataProvider;

        private AllyStateMachine _allyStateMachine;
        private Health _health;

        private ITargetSelector _targetSelector;
        private AllyDragHandler _allyDragSystem;

        private bool _isPaused;
        private Type _stateBeforePause;
        private readonly IPauseService _pauseService;
        private readonly IMergeService _mergeService;

        public Ally(AllyView allyView, AllyModel allyModel, TickableManager tickableManager, IArrowFactory arrowFactory,
            ILevelDataProvider levelDataProvider, IPauseService pauseService, IMergeService mergeService)
        {
            _mergeService = mergeService;
            _pauseService = pauseService;
            _levelDataProvider = levelDataProvider;
            _arrowFactory = arrowFactory;
            _allyView = allyView;
            _allyModel = allyModel;
            _tickableManager = tickableManager;
        }

        public void Initialize()
        {
            _tickableManager.Add(this);
            _allyView.OnDestroyHandler += OnDestroy;
            _allyView.SetController(this);
            _health = new Health(_allyModel.Health);
            _health.OnHealthChanged += HandleHealthChanged;
            InitTargetingSystem();
            InitDragSystem();
            InitializeStateMachine();
            _allyStateMachine.Enter<IdleState>();
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

        private void InitDragSystem()
        {
            _allyDragSystem = new AllyDragHandler(_allyView, this, _levelDataProvider, _mergeService);
            _allyDragSystem.Initialize();
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
            _allyStateMachine.AddState(typeof(IdleState),
                new IdleState(_allyView, _allyModel, _allyStateMachine, _targetSelector));
            _allyStateMachine.AddState(typeof(AttackState),
                new AttackState(_arrowFactory, _allyStateMachine, _targetSelector, _allyView, _allyModel));
            _allyStateMachine.AddState(typeof(DeathState), new DeathState(_allyView));
            _allyStateMachine.AddState(typeof(VictoryState), new VictoryState(_allyView));
            _allyStateMachine.AddState(typeof(PauseState),  new PauseState(_allyView));
        }

        public void Tick()
        {
            if (_isPaused) return;
            _allyStateMachine.UpdateStateLogic();
        }

        public void SetTile(ITile tile) => Tile = tile;
        public void ApplyDamage(float damage) => _health.ApplyDamage(damage);
        public void SetMergeableHighlightActive(bool value) => _allyView.MergeableHighlightVFX.SetActive(value);

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
                _allyStateMachine.Enter<DeathState>();
            }
        }

        public void Destroy() => Object.Destroy(_allyView.gameObject);

        private void OnDestroy()
        {
            _tickableManager.Remove(this);
            _pauseService.RemoveListener(this);
            _allyView.OnDestroyHandler -= Destroy;
            _health.OnHealthChanged -= HandleHealthChanged;
            _targetSelector.Dispose();
        }

        public void OnPause()
        {
            _isPaused = true;
            _stateBeforePause = _allyStateMachine.ActiveStateType;
            _allyStateMachine.Enter<PauseState>();
        }

        public void OnResume()
        {
            _allyStateMachine.Enter(_stateBeforePause);
            _isPaused = false;
            _stateBeforePause = null;
        }
    }
}