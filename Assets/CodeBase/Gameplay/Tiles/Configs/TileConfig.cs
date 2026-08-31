using UnityEngine;

namespace CodeBase.Gameplay.Tiles.Configs
{
    [CreateAssetMenu(menuName = "Tiles/Create Tile Config", fileName = "Tile Config", order = 0)]
    public class TileConfig : ScriptableObject
    {
        public TileView TileView;
        public TileModel TileModel;
    }
}