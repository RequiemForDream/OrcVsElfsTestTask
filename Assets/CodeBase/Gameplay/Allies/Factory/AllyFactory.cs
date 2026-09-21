using CodeBase.Gameplay.Allies.Configs;
using CodeBase.Gameplay.Arrows.Factory;
using CodeBase.Gameplay.Levels;
using CodeBase.Gameplay.Pause;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Allies.Factory
{
    public class AllyFactory : IAllyFactory
    {
        private readonly TickableManager _tickableManager;
        private readonly AllAlliesConfigs _allAlliesConfigs;
        private readonly IArrowFactory _arrowFactory;
        private readonly ILevelDataProvider _levelDataProvider;
        private readonly IPauseService _pauseService;
        private readonly DiContainer _container;

        private int _spawnCounter;

        public AllyFactory(AllAlliesConfigs allAlliesConfigs, DiContainer container)
        {
            _container = container;
            _allAlliesConfigs = allAlliesConfigs;
        }

        public IAlly Create(Vector3 at, AllyType allyType)
        {
            AllyConfig config =  _allAlliesConfigs.AllyConfigs[allyType];
            AllyView allyView = Object.Instantiate(config.AllyView, at, Quaternion.identity);
            Ally ally = _container.Instantiate<Ally>(new object[] { allyView, config.AllyModel });
            ally.SpawnOrder = _spawnCounter++;
            ally.Initialize();
            return ally;
        }
    }
}