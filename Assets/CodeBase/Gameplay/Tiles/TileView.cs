using UnityEngine;

namespace CodeBase.Gameplay.Tiles
{
    public class TileView : MonoBehaviour
    {
        public MeshRenderer MeshRenderer;
        private ITile _tileController;

        public void SetMaterial(Material material) => MeshRenderer.material = material;
        public void SetController(ITile tile) => _tileController = tile;
    }
}