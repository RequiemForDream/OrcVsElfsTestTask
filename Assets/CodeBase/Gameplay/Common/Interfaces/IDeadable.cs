using System;

namespace CodeBase.Gameplay.Common.Interfaces
{
    public interface IDeadable
    {
        event Action<ITarget> OnDie;
    }
}