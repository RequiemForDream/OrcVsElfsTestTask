using UnityEngine;

namespace CodeBase.Gameplay.Tiles
{
    public class TileView : MonoBehaviour
    {
        public MeshRenderer MeshRenderer;

        public void SetMaterial(Material material)
        {
            MeshRenderer.material = material;
        }
    }
}