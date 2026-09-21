using System.Collections.Generic;
using CodeBase.Gameplay.Tutorials.Buy;
using CodeBase.Gameplay.Tutorials.Configs;
using CodeBase.Gameplay.Tutorials.Merge;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Tutorials
{
    public class TutorialsService : ITutorialsService
    {
        private readonly AllTutorialsConfig _allTutorialsConfig;

        private Dictionary<TutorialType, Tutorial> _tutorials;
        private readonly DiContainer _container;

        public TutorialsService(AllTutorialsConfig allTutorialsConfig, DiContainer container)
        {
            _container = container;
            _allTutorialsConfig = allTutorialsConfig;
        }

        public void Initialize()
        {
            _tutorials = new Dictionary<TutorialType, Tutorial>
            {
                { TutorialType.BuyAllyTutorial, _container.Instantiate<BuyAllyTutorial>() },
                { TutorialType.MergeTutorial, _container.Instantiate<MergeTutorial>() },
            };
        }

        public void ShowTutorialByType(TutorialType type)
        {
            if (!_allTutorialsConfig.ShowTutorials) return;
            if (!_tutorials.TryGetValue(type, out Tutorial tutorial))
            {
                Debug.LogError($"Tutorial of type {type} is not registered");
                return;
            }
            
            if (!_allTutorialsConfig.TutorialConfigs.TryGetValue(type, out TutorialConfig config))
            {
                Debug.LogError($"Tutorial config for type {type} not found in AllTutorials");
                return;
            }
            
            if (tutorial.IsCompleted) return;
            
            tutorial.Show(config);
        }
    }
}