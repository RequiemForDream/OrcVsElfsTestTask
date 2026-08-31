using System.Collections.Generic;
using CodeBase.Gameplay.Allies;

namespace CodeBase.Gameplay.AllySpawn
{
    public interface IAllySpawnSystem
    {
        public IReadOnlyDictionary<AllyType, IAlly> AlliesDictionary { get; }
        void Spawn(AllyType allyType);
    }
}