using CodeBase.Gameplay.Allies;
using UnityEngine;

namespace CodeBase.Gameplay.Tiles
{
    public interface ITile
    {
        public bool IsOccupied { get; }
        public IAlly Ally { get; }
        public Transform TileTransform { get; }
        void SetAlly(IAlly ally);
    }
}