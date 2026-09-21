using System;
using CodeBase.Gameplay.Common.Health;
using CodeBase.Gameplay.Common.Interfaces;
using CodeBase.Gameplay.TargetSystem;
using TMPro;
using UnityEngine;

namespace CodeBase.Gameplay.Enemies
{
    public class EnemyView : TargetView
    {
        public override ITarget Target => EnemyController;

        public Collider Collider;
        public Transform ModelCenter;
        public TriggerObserver AttackRangeTrigger;

        public event Action OnDestroyHandler;

        public IEnemy EnemyController {get; private set;}

        [field: SerializeField] public HealthBar HealthBar { get; private set; }
        [field: SerializeField] public EnemyAnimator EnemyAnimator { get; private set; }

        public void SetController(IEnemy enemyController) => EnemyController = enemyController;

        private void OnDestroy()
        {
            OnDestroyHandler?.Invoke();
        }
        
        //Testing
        public TMP_Text Number;

        private void Start()
        {
            Number.text = EnemyController.SpawnOrder.ToString();
        }
    }
}