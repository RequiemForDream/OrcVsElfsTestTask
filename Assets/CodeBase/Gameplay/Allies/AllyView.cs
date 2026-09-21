using System;
using CodeBase.Gameplay.Common.Interfaces;
using CodeBase.Gameplay.TargetSystem;
using UnityEngine;

namespace CodeBase.Gameplay.Allies
{
    public class AllyView : TargetView
    {
        public GameObject MergeableHighlightVFX;
        
        public event Action OnDestroyHandler;
        public override ITarget Target => AllyController;
        
        public Collider Collider;
        public Transform ArrowSpawnPoint;
        public TriggerObserver AttackRangeTrigger;
        public AllyAnimator AllyAnimator;

        public IAlly AllyController {get; private set;}
        public void SetController(IAlly allyController) => AllyController = allyController;

        private void OnDestroy()
        {
            OnDestroyHandler?.Invoke();
        }
    }
}