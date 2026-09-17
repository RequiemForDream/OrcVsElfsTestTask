using UnityEngine.UI;

namespace CodeBase.Gameplay.UI.Windows
{
    public class BuyAllyTutorialWindow : WindowBase
    {
        public Button BuyAlly;

        protected override void OnAwake()
        {
            base.OnAwake();
            BuyAlly.onClick.AddListener(() => Destroy(this.gameObject));
        }

        protected override void Cleanup()
        {
            BuyAlly.onClick.RemoveAllListeners();
            base.Cleanup();
        }
    }
}