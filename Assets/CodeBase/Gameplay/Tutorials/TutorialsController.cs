using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Tutorials
{
    public class TutorialsController : ITutorialsController
    {
        private readonly AllTutorials _allTutorials;

        private Dictionary<TutorialType, Tutorial> _tutorials;
        private readonly DiContainer _container;

        public TutorialsController(AllTutorials allTutorials, DiContainer container)
        {
            _container = container;
            _allTutorials = allTutorials;
        }

        public void Initialize()
        {
            _tutorials = new Dictionary<TutorialType, Tutorial>
            {
                { TutorialType.BuyTutorial, _container.Instantiate<BuyUnitTutorial>() },
                // { TutorialType.MergeTutorial, _container.Instantiate<MergeTutorial>() },
            };
        }

        public void ShowTutorialByType(TutorialType type)
        {
            if (!_tutorials.TryGetValue(type, out Tutorial tutorial))
            {
                Debug.LogError($"Tutorial of type {type} is not registered");
                return;
            }
            
            if (!_allTutorials.TutorialConfigs.TryGetValue(type, out TutorialConfig config))
            {
                Debug.LogError($"Tutorial config for type {type} not found in AllTutorials");
                return;
            }
            
            if (tutorial.IsCompleted) return;
            
            tutorial.Show(config);
        }
    }
}