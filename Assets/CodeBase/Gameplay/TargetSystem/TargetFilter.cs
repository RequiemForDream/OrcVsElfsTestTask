using CodeBase.Common.Enums;
using CodeBase.Gameplay.Common.Interfaces;
using CodeBase.Gameplay.TargetSystem.Interfaces;

namespace CodeBase.Gameplay.TargetSystem
{
    public sealed class TargetFilter : ITargetingFilter
    {
        private readonly TeamId _attackerTeam;

        public TargetFilter(TeamId attackerTeam)
        {
            _attackerTeam = attackerTeam;
        }

        public bool CanTarget(ITarget target) => target != null && target.Team != _attackerTeam;
    }
}