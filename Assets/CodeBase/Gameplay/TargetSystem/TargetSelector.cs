using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Common.Enums;
using CodeBase.Gameplay.Common.Interfaces;
using CodeBase.Gameplay.TargetSystem.Interfaces;
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

                if (_currentTarget != null && !_ignoresTargetedFlag)
                    _currentTarget.IsTargeted = false;

                _currentTarget = value;

                if (_currentTarget != null && !_ignoresTargetedFlag)
                    _currentTarget.IsTargeted = true;

                OnTargetChanged?.Invoke(_currentTarget);
            }
        }

        private ITarget _currentTarget;

        private readonly ITargetsBuffer _targetsBuffer;
        private readonly TargetAttackType _targetAttackType;
        private readonly Transform _attackerPosition;
        private readonly bool _ignoresTargetedFlag;

        public TargetSelector(
            ITargetsBuffer targetsBuffer,
            Transform attackerPosition,
            TargetAttackType targetAttackType,
            bool ignoresTargetedFlag = false)
        {
            _attackerPosition = attackerPosition;
            _targetAttackType = targetAttackType;
            _targetsBuffer = targetsBuffer;
            _ignoresTargetedFlag = ignoresTargetedFlag;
        }

        public void Initialize()
        {
            _targetsBuffer.Initialize();
            _targetsBuffer.OnBufferUpdated += RefreshCurrentTarget;
        }

        private void RefreshCurrentTarget(IReadOnlyCollection<ITarget> targets)
        {
            if (CurrentTarget != null && !CurrentTarget.IsAlive)
                CurrentTarget = null;

            if (CurrentTarget != null)
                return;

            List<ITarget> available = _ignoresTargetedFlag
                ? targets.Where(t => t.IsAlive).ToList()
                : targets.Where(t => !t.IsTargeted && t.IsAlive).ToList();

            CurrentTarget = available.Count > 0
                ? SelectTarget(_attackerPosition.position, available)
                : null;
        }

        private ITarget SelectTarget(Vector3 attackerPosition, List<ITarget> targets)
        {
            return _targetAttackType switch
            {
                TargetAttackType.Nearest => GetNearest(attackerPosition, targets),
                TargetAttackType.LowestHp => GetLowestHp(targets),
                TargetAttackType.HighestHp => GetHighestHp(targets),
                TargetAttackType.First => GetFirst(targets),
                _ => targets.First()
            };
        }

        private ITarget GetNearest(Vector3 attackerPosition, List<ITarget> targets)
        {
            ITarget nearest = targets[0];
            float minDistance = Vector3.Distance(attackerPosition, nearest.Position);

            for (int i = 1; i < targets.Count; i++)
            {
                float distance = Vector3.Distance(attackerPosition, targets[i].Position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = targets[i];
                }
            }

            return nearest;
        }

        private ITarget GetLowestHp(List<ITarget> targets)
        {
            ITarget lowest = targets[0];

            for (int i = 1; i < targets.Count; i++)
            {
                if (targets[i].Health < lowest.Health)
                    lowest = targets[i];
            }

            return lowest;
        }

        private ITarget GetHighestHp(List<ITarget> targets)
        {
            ITarget highest = targets[0];

            for (int i = 1; i < targets.Count; i++)
            {
                if (targets[i].Health > highest.Health)
                    highest = targets[i];
            }

            return highest;
        }
        
        private ITarget GetFirst(List<ITarget> targets)
        {
            ITarget first = targets[0];

            for (int i = 1; i < targets.Count; i++)
            {
                if (targets[i].SpawnOrder < first.SpawnOrder)
                    first = targets[i];
            }

            return first;
        }

        public void Dispose()
        {
            if (_currentTarget != null && !_ignoresTargetedFlag)
                _currentTarget.IsTargeted = false;

            _targetsBuffer.Dispose();
            _targetsBuffer.OnBufferUpdated -= RefreshCurrentTarget;
        }

        public void SetTargetCollectionAllowed(bool allow) => _targetsBuffer.SetEnabled(allow);

        public void ClearBuffer()
        {
            if (_currentTarget != null && !_ignoresTargetedFlag)
                _currentTarget.IsTargeted = false;

            _targetsBuffer.Clear();
        }
    }
}