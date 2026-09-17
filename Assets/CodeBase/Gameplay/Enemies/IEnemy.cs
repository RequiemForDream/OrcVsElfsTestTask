using CodeBase.Gameplay.Common.Interfaces;
using Zenject;
using IPoolable = CodeBase.Infrastructure.Common.Pool;

namespace CodeBase.Gameplay.Enemies
{
    public interface IEnemy : ITickable, IDestroyable, ITarget, IPoolable.IPoolable
    {
        void Initialize();
       
    }
}