using UnityEngine;

namespace CodeBase.Gameplay.Enemies.Configs
{
    [CreateAssetMenu(menuName = "Enemies/Create Enemy Config", fileName = "Enemy Config", order = 0)]
    public class EnemyConfig : ScriptableObject
    {
        public EnemyView EnemyView;
        public EnemyModel EnemyModel;
    }
}