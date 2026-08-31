using System;
using CodeBase.Gameplay.Common.Interfaces;
using CodeBase.Gameplay.TargetSystem;
using UnityEngine;

namespace CodeBase.Gameplay.Allies
{
    public class AllyView : TargetView
    {
        public event Action OnDestroyHandler;
        public override ITarget Target { get; set; }
        
        public Collider Collider;
        public TriggerObserver AttackRangeTrigger;
        public AllyAnimator AllyAnimator;

        public void SetTarget(ITarget target) => Target = target;
        
        private void OnDestroy()
        {
            OnDestroyHandler?.Invoke();
        }
    }
}