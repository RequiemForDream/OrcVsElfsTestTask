using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Common.Enums;
using CodeBase.Gameplay.Common.Interfaces;
using CodeBase.Gameplay.TargetSystem.Interfaces;
using ModestTree;
using UnityEngine;

namespace CodeBase.Gameplay.TargetSystem
{
    public class TargetSelector : ITargetSelector
    {
        public event Action<ITarget> OnTargetChanged;

        public ITarget CurrentTarget
        {
            get => _currentTarget;
            private set
            {
                if (ReferenceEquals(_currentTarget, value)) return;
                _currentTarget = value;
                OnTargetChanged?.Invoke(_currentTarget);
            }
        }
        
        private ITarget _currentTarget;

        private readonly ITargetsBuffer _targetsBuffer;
        private readonly TargetAttackType _targetAttackType;
        private readonly Transform _attackerPosition;

        public TargetSelector(ITargetsBuffer targetsBuffer, Transform attackerPosition, TargetAttackType targetAttackType)
        {
            _attackerPosition = attackerPosition;
            _targetAttackType = targetAttackType;
            _targetsBuffer = targetsBuffer;
        }

        public void Initialize()
        {
            _targetsBuffer.Initialize();
            _targetsBuffer.OnBufferUpdated += RefreshCurrentTarget;
        }

        private void RefreshCurrentTarget(IReadOnlyCollection<ITarget> targets)
        {
            // Debug.Log(targets.Count  + "/" + _targetAttackType);
            if (targets.Count > 0 && (CurrentTarget == null || !CurrentTarget.IsAlive))
            {
                CurrentTarget = GetNearestEnemy(_attackerPosition.position, targets);
            }

            if (targets.Count == 0)
            {
                CurrentTarget = null;
            }
        }

        private ITarget GetNearestEnemy(Vector3 attackerPosition, IReadOnlyCollection<ITarget> targets)
        {
            // ITarget nearestTarget = null;
            // float dist = 0f;
            // foreach (ITarget target in targets)
            // {
            //     float d = Vector3.Distance(attackerPosition, target.Position);
            //     if (d > nearestTarget.Position)
            //     {
            //         dist = d;
            //     }
            // }
            return targets.First();
        }

        public void Dispose()
        {
            _targetsBuffer.Dispose();
            _targetsBuffer.OnBufferUpdated -= RefreshCurrentTarget;
        }

        public void SetTargetCollectionAllowed(bool allow) => _targetsBuffer.SetEnabled(allow);
        public void ClearBuffer() => _targetsBuffer.Clear();
    }
}