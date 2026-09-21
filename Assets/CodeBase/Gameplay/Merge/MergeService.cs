using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Allies;
using CodeBase.Gameplay.AllySpawn;
using CodeBase.Gameplay.Common.Interfaces;
using CodeBase.Gameplay.Tiles;

namespace CodeBase.Gameplay.Merge
{
    public class MergeService : IMergeService
    {
        public event Action OnMerged;

        private readonly IAllySpawnSystem _allySpawnSystem;
        private readonly IMergeRulesProvider _mergeRules;

        public MergeService(IAllySpawnSystem allySpawnSystem, IMergeRulesProvider mergeRules)
        {
            _allySpawnSystem = allySpawnSystem;
            _mergeRules = mergeRules;
        }

        public bool TryMerge(ITile targetTile, ITile sourceTile)
        {
            if (!CanMerge(targetTile, sourceTile))
                return false;

            AllyType currentType = sourceTile.Ally.AllyType;
            AllyType resultType = _mergeRules.GetNextTier(currentType);

            targetTile.Ally.Destroy();
            sourceTile.Ally.Destroy();

            targetTile.Deoccupy();
            sourceTile.Deoccupy();

            _allySpawnSystem.SpawnAtTile(resultType, targetTile);

            OnMerged?.Invoke();
            return true;
        }

        public IReadOnlyList<IMergeable> GetMergeableTiles(ITile sourceTile)
        {
            List<IMergeable> result = new List<IMergeable>();

            foreach (IAlly ally in _allySpawnSystem.Allies)
            {
                ITile tile = ally.Tile;

                if (CanMerge(tile, sourceTile))
                    result.Add(tile.Ally);
            }

            return result;
        }

        public bool CanMerge(ITile targetTile, ITile sourceTile)
        {
            if (targetTile == sourceTile)
                return false;

            if (!targetTile.IsOccupied || !sourceTile.IsOccupied)
                return false;

            AllyType type = sourceTile.Ally.AllyType;

            if (targetTile.Ally.AllyType != type)
                return false;

            return _mergeRules.HasNextTier(type);
        }
    }
}