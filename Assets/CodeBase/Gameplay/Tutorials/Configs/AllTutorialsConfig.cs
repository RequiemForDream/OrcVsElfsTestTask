using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace CodeBase.Gameplay.Tutorials.Configs
{
    [CreateAssetMenu(menuName = "Tutors/Create All Tutorials Config", fileName = "All Tutorials Config", order = 0)]
    public class AllTutorialsConfig : ScriptableObject
    {
        public bool ShowTutorials;
        public SerializedDictionary<TutorialType, TutorialConfig> TutorialConfigs;
    }
}