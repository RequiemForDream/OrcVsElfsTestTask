using CodeBase.Gameplay.Arrows.Config;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Arrows.Factory
{
    public class ArrowFactory : IArrowFactory
    {
        private readonly TickableManager _tickableManager;
        private readonly AllArrowsConfigs _allArrowsConfigs;

        public ArrowFactory(TickableManager tickableManager, AllArrowsConfigs  allArrowsConfigs)
        {
            _tickableManager = tickableManager;
            _allArrowsConfigs = allArrowsConfigs;
        }
        
        public Arrow Create(Vector3 at, ArrowType type)
        {
            ArrowConfig config = _allArrowsConfigs.Arrows[type];

            ArrowView view = Object.Instantiate(config.ArrowView, at, Quaternion.identity);
            
            Arrow arrow = new Arrow(config.ArrowModel, view, _tickableManager);
            
            arrow.Initialize();
            
            return arrow;
        }
    }
}