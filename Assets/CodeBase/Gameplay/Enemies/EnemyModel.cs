using System;
using CodeBase.Common.Enums;

namespace CodeBase.Gameplay.Enemies
{
    [Serializable]
    public class EnemyModel
    {
        public TeamId TeamId;
        public float Speed = 5f;
        public float Health;
        public float TurnSpeed = 10f;
        public float DistanceThreshold = 0.2f;
        public TargetAttackType TargetAttackType;
    }
}