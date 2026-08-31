using System;
using CodeBase.Gameplay.Common.Interfaces;
using Zenject;

namespace CodeBase.Gameplay.Arrows
{
    public interface IArrow : ITickable, IDestroyable
    {
        event Action OnHit;
        void Release(ITarget target);
    }
}