// using CodeBase.Gameplay.Enemies;
// using CodeBase.Gameplay.Enemies.Factory;
// using UnityEngine;
//
// namespace CodeBase.Infrastructure.Common.Pool
// {
//     public class EnemyPool : Pool<Vector2, IEnemy>
//     {
//         private readonly IEnemyFactory _enemyFactory;
//
//         public EnemyPool(int count, Transform container, IEnemyFactory enemyFactory) : base( count, container)
//         {
//             _enemyFactory = enemyFactory;
//         }
//
//         public IEnemy CreateObject<Vector2, EnemyType>(Vector3 arg1, EnemyType arg2, bool isActiveByDefault = false)
//         {
//             IEnemy createdObject = new Enemy();
//             createdObject.SetActive(isActiveByDefault);
//             _pool.Add(createdObject);
//             return createdObject;
//         }
//     }
// }