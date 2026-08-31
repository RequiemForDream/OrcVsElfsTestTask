using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Allies.Factory
{
    public interface IAllyFactory : IFactory<Vector3, AllyType, IAlly>
    {
        
    }
}