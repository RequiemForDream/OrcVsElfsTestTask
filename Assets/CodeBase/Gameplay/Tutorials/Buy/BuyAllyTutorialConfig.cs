using CodeBase.Gameplay.Tutorials.Configs;
using CodeBase.Gameplay.UI.Windows;
using UnityEngine;

namespace CodeBase.Gameplay.Tutorials.Buy
{
    [CreateAssetMenu(menuName = "Tutors/Create Buy Unit Tutorial", fileName = "Buy Unit Tutorial Config", order = 0)]
    public class BuyAllyTutorialConfig : TutorialConfig
    {
        public WindowID TutorialWindow;
    }
}