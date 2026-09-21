using System;

namespace CodeBase.Gameplay.Common.Interfaces
{
    public interface IDestroyable
    {
        event Action OnDestroyHandler;
        void Destroy();
    }
}