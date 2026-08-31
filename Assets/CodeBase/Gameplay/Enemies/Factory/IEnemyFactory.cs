using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Enemies.Factory
{
    public interface IEnemyFactory : IFactory<Vector3, EnemyType, IEnemy>
    {
    }
}