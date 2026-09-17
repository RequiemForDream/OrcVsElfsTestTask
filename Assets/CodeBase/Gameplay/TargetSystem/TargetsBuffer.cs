using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Common.Interfaces;
using CodeBase.Gameplay.TargetSystem.Interfaces;
using UnityEngine;

namespace CodeBase.Gameplay.TargetSystem
{
    public class TargetsBuffer : ITargetsBuffer
    {
        public event Action<HashSet<ITarget>> OnBufferUpdated;

        private readonly TriggerObserver _triggerObserver;
        
        private readonly ITargetingFilter _targetingFilter;

        private readonly HashSet<ITarget> _targets = new();

        public TargetsBuffer(TriggerObserver triggerObserver, ITargetingFilter targetingFilter)
        {
            _triggerObserver = triggerObserver;
            _targetingFilter = targetingFilter;
        }

        public void Initialize()
        {
            _triggerObserver.TriggerEnter += OnTriggerEnter;
            _triggerObserver.TriggerExit += OnTriggerExit;
        }

        public void SetEnabled(bool enabled) => _triggerObserver.Collider.enabled = enabled;

        private void RemoveTarget(ITarget target)
        {
            _targets.Remove(target);
            target.OnDie -= RemoveTarget;
            OnBufferUpdated?.Invoke(_targets);
        }

        private void OnTriggerEnter(Collider obj)
        {
            if (obj.TryGetComponent(out TargetView target))
            {
                if (_targetingFilter.CanTarget(target.Target))
                {
                    AddTarget(target.Target);
                }
            }
        }

        private void AddTarget(ITarget target)
        {
            _targets.Add(target);   
            target.OnDie += RemoveTarget;
            OnBufferUpdated?.Invoke(_targets);
        }

        private void OnTriggerExit(Collider obj)
        {
            if (obj.TryGetComponent(out TargetView target))
            {
                RemoveTarget(target.Target);
            }
        }
        
        public void Clear() => _targets.Clear();

        public void Dispose()
        {
            _triggerObserver.TriggerEnter -= OnTriggerEnter;
            _triggerObserver.TriggerExit -= OnTriggerExit;
        }
    }
}