using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Arrows.Factory
{
    public interface IArrowFactory : IFactory<Vector3, ArrowType, Arrow>
    {
        
    }
}