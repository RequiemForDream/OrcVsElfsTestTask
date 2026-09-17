using CodeBase.Gameplay.UI.Windows;
using UnityEngine;

namespace CodeBase.Gameplay.Tutorials
{
    [CreateAssetMenu(menuName = "Tutors/Create Buy Unit Tutorial", fileName = "Buy Unit Tutorial Config", order = 0)]
    public class BuyUnitTutorialConfig : TutorialConfig
    {
        public WindowID TutorialWindow;
    }
}