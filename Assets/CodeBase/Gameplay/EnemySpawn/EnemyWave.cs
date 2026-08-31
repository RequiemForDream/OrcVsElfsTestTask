using System;
using CodeBase.Gameplay.Enemies.Factory;
using UnityEngine;

namespace CodeBase.Gameplay.EnemySpawn
{
    [CreateAssetMenu(menuName = "Enemies/Spawn/Create Enemy Wave", fileName = "Enemy Wave")]
    public class EnemyWave : ScriptableObject
    {
        public EnemySpawnSequence[] EnemySpawnSequences;

        public State Begin(IEnemyFactory enemyFactory) => new State(this, enemyFactory);
        
        [Serializable]
        public struct State
        {
            private EnemyWave _wave;
            private int _index;
            private EnemySpawnSequence.State _sequence;
            private IEnemyFactory _enemyFactory;

            public State(EnemyWave wave, IEnemyFactory enemyFactory)
            {
                _enemyFactory = enemyFactory;
                _wave = wave;
                _index = 0;
                _sequence = _wave.EnemySpawnSequences[0].Begin(_enemyFactory);
            }

            public float Progress(float deltaTime)
            {
                deltaTime = _sequence.Progress(deltaTime);
                while (deltaTime >= 0f)
                {
                    if (++_index >= _wave.EnemySpawnSequences.Length)
                    {
                        return deltaTime;
                    }
                    
                    _sequence = _wave.EnemySpawnSequences[_index].Begin(_enemyFactory);
                    deltaTime = _sequence.Progress(deltaTime);
                } 
                
                return -1f;
            }
        }
    }
}