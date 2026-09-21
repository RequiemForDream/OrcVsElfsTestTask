using CodeBase.Gameplay.UI.Windows;
using UnityEngine.UI;

namespace CodeBase.Gameplay.Tutorials.Buy
{
    public class BuyAllyTutorialWindow : WindowBase
    {
        public Button BuyAlly;

        protected override void Cleanup()
        {
            BuyAlly.onClick.RemoveAllListeners();
            base.Cleanup();
        }
    }
}