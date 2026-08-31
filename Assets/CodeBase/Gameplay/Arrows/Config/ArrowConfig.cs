using UnityEngine;

namespace CodeBase.Gameplay.Arrows.Config
{
    [CreateAssetMenu(menuName = "Arrow/Create Arrow Config", fileName = "Arrow Config", order = 0)]
    public class ArrowConfig : ScriptableObject
    {
        public ArrowView ArrowView;
        public ArrowModel ArrowModel;
    }
}