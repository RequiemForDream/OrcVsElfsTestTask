using UnityEngine;

namespace CodeBase.Gameplay.Allies.Configs
{
    [CreateAssetMenu(menuName = "Allies/Create Ally Config", fileName = "Ally Config", order = 0)]
    public class AllyConfig : ScriptableObject
    {
        public AllyView AllyView;
        public AllyModel AllyModel;
    }
}