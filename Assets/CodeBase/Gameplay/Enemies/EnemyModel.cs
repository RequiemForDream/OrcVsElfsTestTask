using System;
using CodeBase.Common.Enums;

namespace CodeBase.Gameplay.Enemies
{
    [Serializable]
    public class EnemyModel
    {
        public TeamId TeamId;
        public float Speed = 5f;
        public int Reward = 10;
        public float Health;
        public float TurnSpeed = 10f;
        public float PathDistanceThreshold = 0.2f;
        public float AttackRange = 1.5f;
        public float Damage = 10f;
        public TargetAttackType TargetAttackType;
    }
}