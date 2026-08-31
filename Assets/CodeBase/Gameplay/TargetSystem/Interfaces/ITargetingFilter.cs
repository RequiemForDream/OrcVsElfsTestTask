using CodeBase.Gameplay.Common.Interfaces;

namespace CodeBase.Gameplay.TargetSystem.Interfaces
{
    public interface ITargetingFilter
    {
        bool CanTarget(ITarget target);
    }
}