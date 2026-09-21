using CodeBase.Gameplay.Currency;
using CodeBase.Gameplay.Enemies.Configs;
using CodeBase.Gameplay.Levels;
using CodeBase.Gameplay.Pause;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Enemies.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly AllEnemiesConfigs _allEnemiesConfigs;
        private readonly DiContainer _diContainer;
        private readonly ILevelDataProvider _levelDataProvider;

        private int _spawnCounter;

        public EnemyFactory(DiContainer diContainer, AllEnemiesConfigs allEnemiesConfigs, ILevelDataProvider levelDataProvider)
        {
            _levelDataProvider = levelDataProvider;
            _allEnemiesConfigs = allEnemiesConfigs;
            _diContainer = diContainer;
        }

        public IEnemy Create(Vector3 at, EnemyType enemyType)
        {
            EnemyConfig enemyConfig = _allEnemiesConfigs.Enemies[enemyType];
            EnemyView enemyView = Object.Instantiate(enemyConfig.EnemyView, at, Quaternion.identity);
            Enemy enemy = _diContainer.Instantiate<Enemy>(new object[] { enemyView, enemyConfig.EnemyModel, _levelDataProvider.EnemyWalkPath });
            enemy.SpawnOrder = _spawnCounter++;
            enemy.Initialize();

            return enemy;
        }
    }
}