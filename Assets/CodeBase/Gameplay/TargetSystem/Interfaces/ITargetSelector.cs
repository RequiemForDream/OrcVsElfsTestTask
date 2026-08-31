using System;
using CodeBase.Gameplay.Common.Interfaces;

namespace CodeBase.Gameplay.TargetSystem.Interfaces
{
    public interface ITargetSelector : IDisposable
    {
        ITarget CurrentTarget { get;  }
        event Action OnTargetChanged;
        void Initialize();
    }
}