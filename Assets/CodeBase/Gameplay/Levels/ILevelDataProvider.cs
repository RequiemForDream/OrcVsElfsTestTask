using CurvedPathGenerator;
using UnityEngine;

namespace CodeBase.Gameplay.Levels
{
    public interface ILevelDataProvider
    {
        Vector3 StartPoint { get; }
        PathGenerator EnemyWalkPath { get; }
        void SetStartPoint(Vector3 startPoint);
        void SetEnemyWalkPath(PathGenerator enemyWalkPath);
    }
}