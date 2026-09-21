using System;
using CodeBase.Gameplay.Enemies.Factory;
using CodeBase.Gameplay.Pause;
using Zenject;

namespace CodeBase.Gameplay.EnemySpawn
{
    public class EnemySpawnSystem :  IEnemySpawnSystem, IPauseListener
    {
        private readonly TickableManager _tickableManager;
        private readonly GameScenario _gameScenario;
        
        private GameScenario.State _activeScenario;
        private readonly IEnemyFactory _enemyFactory;
        
        private bool _isPaused;
        private readonly IPauseService _pauseService;

        public EnemySpawnSystem(GameScenario gameScenario, TickableManager tickableManager, IEnemyFactory enemyFactory,
            IPauseService pauseService)
        {
            _pauseService = pauseService;
            _enemyFactory = enemyFactory;
            _tickableManager = tickableManager;
            _gameScenario = gameScenario;
        }

        public void StartScenario()
        {
            _pauseService.AddListener(this);
            _activeScenario = _gameScenario.Begin(_enemyFactory);
            _tickableManager.Add(this);
        }

        public void Tick()
        {
            if (_isPaused) return;
            _activeScenario.Progress();
        }

        public void OnPause()
        {
            _isPaused = true;
        }

        public void OnResume()
        {
            _isPaused = false;
        }
    }
}