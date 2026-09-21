using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Common.Interfaces;

namespace CodeBase.Gameplay.TargetSystem.Interfaces
{
    public interface ITargetsBuffer : IDisposable
    {
        event Action<IReadOnlyList<ITarget>> OnBufferUpdated;
        void Initialize();
        void SetEnabled(bool enabled);
        void Clear();
    }
}