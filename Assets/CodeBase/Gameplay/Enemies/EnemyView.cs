using System;
using CodeBase.Gameplay.Common.Health;
using CodeBase.Gameplay.Common.Interfaces;
using CodeBase.Gameplay.TargetSystem;
using UnityEngine;

namespace CodeBase.Gameplay.Enemies
{
    public class EnemyView : TargetView
    {
        public override ITarget Target { get; set; }
        
        public Collider Collider;
        public Transform ModelCenter;
        public TriggerObserver AttackRangeTrigger;

        public event Action OnDestroyHandler;

        [field: SerializeField] public HealthBar HealthBar { get; private set; }
        [field: SerializeField] public EnemyAnimator EnemyAnimator { get; private set; }

        public void SetController(ITarget target) => Target = target;

        private void OnDestroy()
        {
            OnDestroyHandler?.Invoke();
        }
    }
}