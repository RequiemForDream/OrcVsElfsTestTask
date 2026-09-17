using CodeBase.Gameplay.Enemies.Factory;
using Zenject;

namespace CodeBase.Gameplay.EnemySpawn
{
    public class EnemySpawnSystem :  IEnemySpawnSystem
    {
        private readonly TickableManager _tickableManager;
        private readonly GameScenario _gameScenario;
        
        private GameScenario.State _activeScenario;
        private readonly IEnemyFactory _enemyFactory;

        public EnemySpawnSystem(GameScenario gameScenario, TickableManager tickableManager, IEnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
            _tickableManager = tickableManager;
            _gameScenario = gameScenario;
        }

        public void StartScenario()
        {
            _activeScenario = _gameScenario.Begin(_enemyFactory);
            _tickableManager.Add(this);
        }

        public void Tick()
        {
            _activeScenario.Progress();
        }
    }
}