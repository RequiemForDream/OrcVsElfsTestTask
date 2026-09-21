using UnityEngine;

namespace CodeBase.Gameplay.Tiles
{
    public class TileView : MonoBehaviour
    {
        public MeshRenderer MeshRenderer;
        public ITile TileController {get; private set;}

        public void SetMaterial(Material material) => MeshRenderer.material = material;
        public void SetController(ITile tile) => TileController = tile;
    }
}