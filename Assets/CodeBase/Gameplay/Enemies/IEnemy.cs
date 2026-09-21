using CodeBase.Gameplay.Common.Interfaces;
using CodeBase.Gameplay.Pause;
using Zenject;
using IPoolable = CodeBase.Infrastructure.Common.Pool;

namespace CodeBase.Gameplay.Enemies
{
    public interface IEnemy : ITickable, IDestroyable, ITarget, IPauseListener
    {
        void Initialize();
       
    }
}