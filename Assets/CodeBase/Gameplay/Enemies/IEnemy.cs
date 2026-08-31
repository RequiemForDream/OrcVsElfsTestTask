using CodeBase.Gameplay.Common.Interfaces;
using Zenject;

namespace CodeBase.Gameplay.Enemies
{
    public interface IEnemy : ITickable, IDestroyable, ITarget
    {
        void Initialize();
       
    }
}