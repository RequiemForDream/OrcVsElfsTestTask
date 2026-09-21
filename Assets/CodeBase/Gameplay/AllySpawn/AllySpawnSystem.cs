using System.Collections.Generic;
using CodeBase.Gameplay.Allies;
using CodeBase.Gameplay.Allies.Factory;
using CodeBase.Gameplay.Board;
using CodeBase.Gameplay.Tiles;

namespace CodeBase.Gameplay.AllySpawn
{
    public class AllySpawnSystem : IAllySpawnSystem
    {
        public IReadOnlyList<IAlly> Allies => _allies;
        
        private readonly IAllyFactory _allyFactory;
        private readonly IBoardSystem _boardSystem;

        private readonly List<IAlly> _allies = new(4);

        public AllySpawnSystem(IAllyFactory allyFactory, IBoardSystem boardSystem)
        {
            _boardSystem = boardSystem;
            _allyFactory = allyFactory;
        }

        public void Spawn(AllyType allyType)
        {
            if (_boardSystem.HasFreeTile(out ITile tile))
            {
                IAlly ally = _allyFactory.Create(tile.TileTransform.position, allyType);
                ally.SetTile(tile);
                tile.SetAlly(ally);
                _allies.Add(ally);
            }
        }

        public void SpawnAtTile(AllyType allyType, ITile tile)
        {
            IAlly ally = _allyFactory.Create(tile.TileTransform.position, allyType);
            ally.SetTile(tile);
            tile.SetAlly(ally);
            _allies.Add(ally);
        }
    }
}