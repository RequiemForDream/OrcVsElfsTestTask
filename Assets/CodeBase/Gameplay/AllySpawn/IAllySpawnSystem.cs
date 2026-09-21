using System.Collections.Generic;
using CodeBase.Gameplay.Allies;
using CodeBase.Gameplay.Tiles;

namespace CodeBase.Gameplay.AllySpawn
{
    public interface IAllySpawnSystem
    {
        IReadOnlyList<IAlly> Allies { get; }
        void Spawn(AllyType allyType);
        void SpawnAtTile(AllyType allyType, ITile tile);
    }
}