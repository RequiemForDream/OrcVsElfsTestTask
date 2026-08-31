using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace CodeBase.Gameplay.Arrows.Config
{
    [CreateAssetMenu(menuName = "Arrow/Create All Arrows Configs", fileName = "All Arrows Configs", order = 0)]
    public class AllArrowsConfigs : ScriptableObject
    {
        public SerializedDictionary<ArrowType, ArrowConfig> Arrows;
    }
}