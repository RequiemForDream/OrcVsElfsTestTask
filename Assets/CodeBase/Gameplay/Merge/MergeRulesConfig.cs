using AYellowpaper.SerializedCollections;
using CodeBase.Gameplay.Allies;
using UnityEngine;

namespace CodeBase.Gameplay.Merge
{
    [CreateAssetMenu(menuName = "Merge/Merge Rules Config", fileName = "Merge Rules Config")]
    public class MergeRulesConfig : ScriptableObject, IMergeRulesProvider
    {
        [SerializeField] private SerializedDictionary<AllyType, AllyType> _nextTierMap;

        public bool HasNextTier(AllyType type) => _nextTierMap.ContainsKey(type);

        public AllyType GetNextTier(AllyType type)
        {
            if (!_nextTierMap.TryGetValue(type, out AllyType nextTier))
            {
                Debug.LogError($"No next tier configured for AllyType {type}");
                return type; 
            }

            return nextTier;
        }
    }
}