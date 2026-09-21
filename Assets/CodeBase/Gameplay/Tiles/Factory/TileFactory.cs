using CodeBase.Gameplay.Tiles.Configs;
using UnityEngine;

namespace CodeBase.Gameplay.Tiles.Factory
{
    public class TileFactory : ITileFactory
    {
        private TileConfig _tileConfig;

        public TileFactory(TileConfig tileConfig)
        {
            _tileConfig = tileConfig;
        }
        
        public ITile Create(Vector3 at, Transform parent)
        {
            TileView tileView = Object.Instantiate(_tileConfig.TileView, at, Quaternion.identity,  parent);
            ITile tile = new Tile(tileView, _tileConfig.TileModel);
            tile.Initialize();
            return tile;
        }
    }
}