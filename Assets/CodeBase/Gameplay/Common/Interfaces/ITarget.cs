using CodeBase.Common.Enums;
using UnityEngine;

namespace CodeBase.Gameplay.Common.Interfaces
{
    public interface ITarget : IDamageable, IHealthable, IDeadable
    {
        TeamId Team { get; }
        Vector3 Position { get; }
        Vector3 HitPosition { get; }
         bool IsTargeted { get; set; }
         int SpawnOrder { get; set; }
        
    }
}