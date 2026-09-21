using System;
using CodeBase.Common.Enums;
using CodeBase.Gameplay.Arrows;
using UnityEngine;

namespace CodeBase.Gameplay.Allies
{
    [Serializable]
    public class AllyModel
    {
        public AllyType AllyType = AllyType.Archer;
        public TeamId TeamId;
        public float Health = 1;
        public float Damage;
        public ArrowType ArrowType;
        public TargetAttackType TargetAttackType;
    }
}