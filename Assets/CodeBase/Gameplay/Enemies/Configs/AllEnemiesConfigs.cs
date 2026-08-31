using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace CodeBase.Gameplay.Enemies.Configs
{
    [CreateAssetMenu(menuName = "Enemies/Create All Enemies Config", fileName = "All Enemies Config", order = 0)]
    public class AllEnemiesConfigs : ScriptableObject
    {
        public SerializedDictionary<EnemyType, EnemyConfig> Enemies;
    }
}