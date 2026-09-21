using CodeBase.Gameplay.Allies;
using UnityEngine;

namespace CodeBase.Gameplay.Tiles
{
    public class Tile : ITile
    {
        private readonly TileView _tileView;
        private readonly TileModel _tileModel;

        public bool IsOccupied => Ally != null;

        public IAlly Ally { get; private set; }

        public Transform TileTransform => _tileView.transform;

        public Tile(TileView tileView, TileModel tileModel)
        {
            _tileView = tileView;
            _tileModel = tileModel;
        }

        public void Initialize()
        {
            _tileView.SetController(this);
        }

        public void SetAlly(IAlly ally)
        {
            Ally = ally;
            _tileView.SetMaterial(_tileModel.CellOccupied);
        }

        public void Deoccupy()
        {
            Ally = null;
            _tileView.SetMaterial(_tileModel.CellFree);
        }
    }
}