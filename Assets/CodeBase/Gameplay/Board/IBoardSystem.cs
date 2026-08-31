using System.Collections.Generic;
using CodeBase.Gameplay.Tiles;

namespace CodeBase.Gameplay.Board
{
    public interface IBoardSystem
    {
        void Generate();
        IReadOnlyList<ITile> Tiles { get; }
        bool HasFreeTile(out ITile tile);
    }
}