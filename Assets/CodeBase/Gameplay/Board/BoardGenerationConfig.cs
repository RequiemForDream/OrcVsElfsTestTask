using UnityEngine;

namespace CodeBase.Gameplay.Board
{
    [CreateAssetMenu(menuName = "Board/Create Board Generation Config", fileName = "Board Generation Config", order = 0)]
    public class BoardGenerationConfig : ScriptableObject
    {
        public int Rows = 4;
        public int Columns = 5;
        public float OffsetX = 3f;
        public float OffsetZ = 3f;
    }
}