using CodeBase.Gameplay.Common.Interfaces;
using CodeBase.Gameplay.Tiles;
using Zenject;

namespace CodeBase.Gameplay.Allies
{
    public interface IAlly : ITarget, ITickable, IDestroyable
    {
        ITile Tile { get; }
        void Initialize();
        void SetTile(ITile tile);
    }
}