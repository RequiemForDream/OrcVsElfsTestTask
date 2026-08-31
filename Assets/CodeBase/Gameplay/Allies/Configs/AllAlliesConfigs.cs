using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace CodeBase.Gameplay.Allies.Configs
{
    [CreateAssetMenu(menuName = "Allies/Create All Allies Configs", fileName = "All Allies Configs", order = 0)]
    public class AllAlliesConfigs : ScriptableObject
    {
        public SerializedDictionary<AllyType, AllyConfig> AllyConfigs;
    }
}