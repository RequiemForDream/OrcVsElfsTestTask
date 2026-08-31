using System;
using CodeBase.Gameplay.Enemies;
using CodeBase.Gameplay.Enemies.Factory;
using UnityEngine;

namespace CodeBase.Gameplay.EnemySpawn
{
    [Serializable]
    public class EnemySpawnSequence
    {
        public EnemyType EnemyType;
        [Range(1, 100)] public int Amount = 10;
        [Range(0.1f, 10f)] public float Cooldown = 1f;
        
        public State Begin(IEnemyFactory enemyFactory) => new State(this, enemyFactory);

        [Serializable]
        public struct State
        {
            private EnemySpawnSequence _sequence;
            private int _count;
            private float _cooldown;
            private IEnemyFactory _enemyFactory;

            public State(EnemySpawnSequence sequence, IEnemyFactory enemyFactory)
            {
                _enemyFactory = enemyFactory;
                _sequence = sequence;
                _count = 0;
                _cooldown = _sequence.Cooldown;
            }

            public float Progress(float deltaTime)
            {
                _cooldown += deltaTime;
                while (_cooldown >= _sequence.Cooldown)
                {
                    _cooldown -= _sequence.Cooldown;
                    if (_count >= _sequence.Amount)
                    {
                        return _cooldown;
                    }
                    
                    _count++;
                    _enemyFactory.Create(Vector3.zero, _sequence.EnemyType);
                    Debug.Log("Enemy spawned");
                    //Spawn Enemy
                }

                return -1f;
            }
        }
    }
}