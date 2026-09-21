using System.Collections.Generic;
using System.Linq;
using CodeBase.Gameplay.Common.Random;
using CodeBase.Gameplay.Tiles;
using CodeBase.Gameplay.Tiles.Factory;
using UnityEngine;

namespace CodeBase.Gameplay.Board
{
    public class BoardSystem : IBoardSystem
    {
        public IReadOnlyList<ITile> Tiles => _tiles;
        
        private readonly List<ITile> _tiles = new(20);
        private readonly ITileFactory _tileFactory;
        private readonly BoardGenerationConfig _boardGenerationConfig;
        private readonly IRandomService _randomService;

        private readonly List<int> _startPositionsToSpawn = new List<int>{6, 17};

        public BoardSystem(ITileFactory tileFactory, BoardGenerationConfig boardGenerationConfig, IRandomService randomService)
        {
            _randomService = randomService;
            _boardGenerationConfig = boardGenerationConfig;
            _tileFactory = tileFactory;
        }
        
        public void Generate()
        {
            GameObject parent = new GameObject("Garden");
            
            for (int i = 0; i < _boardGenerationConfig.Columns * _boardGenerationConfig.Rows; i++)
            {
                float xPosition = _boardGenerationConfig.OffsetX * (i % _boardGenerationConfig.Columns);
                float zPosition = _boardGenerationConfig.OffsetZ * (i / _boardGenerationConfig.Columns);
                Vector3 positionToCreate = new Vector3(xPosition, 0.05f, zPosition);

                ITile tile = _tileFactory.Create(positionToCreate, parent.transform);
                _tiles.Add(tile);
            }
        }

        public bool HasFreeTile(out ITile tile)
        {
            if (_startPositionsToSpawn.Count > 0)
            {
                tile = _tiles[_startPositionsToSpawn[0]];
                _startPositionsToSpawn.RemoveAt(0);
                return true;
            }
            List<ITile> freeTiles = _tiles.Where(x => !x.IsOccupied).ToList();
            
            if (freeTiles.Count == 0)
            {
                tile = null;
                return false;
            }

            tile = freeTiles[_randomService.Range(0, freeTiles.Count)];
            return true;
        }
    }
}