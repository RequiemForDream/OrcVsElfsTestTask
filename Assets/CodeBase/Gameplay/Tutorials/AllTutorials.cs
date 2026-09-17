using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace CodeBase.Gameplay.Tutorials
{
    [CreateAssetMenu(menuName = "Tutors/Create All Tutorials Config", fileName = "All Tutorials Config", order = 0)]
    public class AllTutorials : ScriptableObject
    {
        public SerializedDictionary<TutorialType, TutorialConfig> TutorialConfigs;
    }
}