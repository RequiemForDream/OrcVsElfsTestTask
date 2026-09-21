using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Allies;
using CodeBase.Gameplay.Common.Interfaces;
using CodeBase.Gameplay.Tiles;

namespace CodeBase.Gameplay.Merge
{
    public interface IMergeService
    {
        event Action OnMerged;
        bool TryMerge(ITile targetTile, ITile sourceTile);
        bool CanMerge(ITile targetTile, ITile sourceTile);
        IReadOnlyList<IMergeable> GetMergeableTiles(ITile sourceTile);
    }
}