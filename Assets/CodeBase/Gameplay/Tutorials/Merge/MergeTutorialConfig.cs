using CodeBase.Gameplay.Tutorials.Configs;
using CodeBase.Gameplay.UI.Windows;
using UnityEngine;

namespace CodeBase.Gameplay.Tutorials.Merge
{
    [CreateAssetMenu(menuName = "Tutors/Create Merge Tutorial Config", fileName = "Merge Tutorial Config", order = 0)]
    public class MergeTutorialConfig : TutorialConfig
    {
        public WindowID WindowID;
    }
}