using System;
using CodeBase.Gameplay.Enemies.Factory;
using UnityEngine;

namespace CodeBase.Gameplay.EnemySpawn
{
    [CreateAssetMenu(menuName = "Enemies/Spawn/Create Game Scenario", fileName = "Game Scenario")]
    public class GameScenario : ScriptableObject
    {
        public EnemyWave[] EnemyWaves;
        
        public State Begin(IEnemyFactory enemyFactory) => new State(this, enemyFactory);
        
        [Serializable]
        public struct State
        {
            private GameScenario _scenario;
            private int _index;
            private EnemyWave.State _wave;
            private IEnemyFactory _enemyFactory;

            public State(GameScenario scenario, IEnemyFactory enemyFactory)
            {
                _enemyFactory = enemyFactory;
                _scenario = scenario;
                _index = 0;
                _wave = _scenario.EnemyWaves[0].Begin(_enemyFactory);
            }

            public bool Progress()
            {
                float deltaTime = _wave.Progress(Time.deltaTime);
                while (deltaTime > 0f)
                {
                    if (++_index >= _scenario.EnemyWaves.Length)
                    {
                        return false;
                    }               
                    
                    _wave = _scenario.EnemyWaves[_index].Begin(_enemyFactory);
                    deltaTime = _wave.Progress(deltaTime);
                }
                
                return true;
            }
        }
    }
}