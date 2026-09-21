using CodeBase.Gameplay.Common.Interfaces;
using CodeBase.Gameplay.Pause;
using CodeBase.Gameplay.Tiles;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Allies
{
    public interface IAlly : ITarget, ITickable, IDestroyable, IPauseListener, IMergeable
    {
        ITile Tile { get; }
        void Initialize();
        void SetTile(ITile tile);
        AllyType AllyType { get; }
        Transform Transform { get;  }
    }
}