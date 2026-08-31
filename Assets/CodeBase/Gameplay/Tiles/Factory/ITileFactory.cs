using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Tiles.Factory
{
    public interface ITileFactory : IFactory<Vector3, Transform, ITile>
    {
        
    }
}