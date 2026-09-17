using CodeBase.Gameplay.Currency;
using CodeBase.Gameplay.Enemies.Configs;
using CodeBase.Gameplay.Levels;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Enemies.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly ILevelDataProvider _levelDataProvider;
        private readonly TickableManager _tickableManager;
        private readonly AllEnemiesConfigs _allEnemiesConfigs;
        private readonly ICurrencyCounter _currencyCounter;

        public EnemyFactory(ILevelDataProvider levelDataProvider, TickableManager tickableManager, AllEnemiesConfigs allEnemiesConfigs, ICurrencyCounter currencyCounter)
        {
            _currencyCounter = currencyCounter;
            _allEnemiesConfigs = allEnemiesConfigs;
            _tickableManager = tickableManager;
            _levelDataProvider = levelDataProvider;
        }
        
        public IEnemy Create(Vector3 at, EnemyType enemyType)
        {
            EnemyConfig enemyConfig = _allEnemiesConfigs.Enemies[enemyType];
            EnemyView enemyView = Object.Instantiate(enemyConfig.EnemyView, at, Quaternion.identity);
            
            Enemy enemy = new Enemy(enemyView, enemyConfig.EnemyModel, _levelDataProvider.EnemyWalkPath, _currencyCounter);
            
            _tickableManager.Add(enemy);
            enemy.Initialize();
            
            return enemy;
        }
    }
}