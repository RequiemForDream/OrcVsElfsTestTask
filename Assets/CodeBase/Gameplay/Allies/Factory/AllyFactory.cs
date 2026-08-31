using CodeBase.Gameplay.Allies.Configs;
using CodeBase.Gameplay.Arrows.Factory;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Allies.Factory
{
    public class AllyFactory : IAllyFactory
    {
        private readonly TickableManager _tickableManager;
        private readonly AllAlliesConfigs _allAlliesConfigs;
        private readonly IArrowFactory _arrowFactory;


        public AllyFactory(TickableManager tickableManager, AllAlliesConfigs allAlliesConfigs, IArrowFactory arrowFactory)
        {
            _tickableManager = tickableManager;
            _allAlliesConfigs = allAlliesConfigs;
            _arrowFactory = arrowFactory;
        }

        public IAlly Create(Vector3 at, AllyType allyType)
        {
            AllyConfig config =  _allAlliesConfigs.AllyConfigs[allyType];
            AllyView allyView = Object.Instantiate(config.AllyView, at, Quaternion.identity);
            Ally ally = new Ally(allyView, config.AllyModel, _tickableManager, _arrowFactory);
            ally.Initialize();
            return ally;
        }
    }
}