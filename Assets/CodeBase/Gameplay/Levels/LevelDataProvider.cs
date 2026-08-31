using CurvedPathGenerator;
using UnityEngine;

namespace CodeBase.Gameplay.Levels
{
    public class LevelDataProvider : ILevelDataProvider
    {
        public Vector3 StartPoint { get; private set; }
        public PathGenerator EnemyWalkPath { get; private set; }
        
        public void SetStartPoint(Vector3 startPoint)
        {
           StartPoint = startPoint;
        }

        public void SetEnemyWalkPath(PathGenerator enemyWalkPath)
        {
            EnemyWalkPath = enemyWalkPath;
        }
    }
}