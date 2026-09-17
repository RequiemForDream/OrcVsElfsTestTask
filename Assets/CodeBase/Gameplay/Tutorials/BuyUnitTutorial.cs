using CodeBase.Gameplay.Allies;
using CodeBase.Gameplay.Allies.Purchase;
using CodeBase.Gameplay.AllySpawn;
using CodeBase.Gameplay.UI.Factory;
using CodeBase.Gameplay.UI.Windows;

namespace CodeBase.Gameplay.Tutorials
{
    public class BuyUnitTutorial : Tutorial<BuyUnitTutorialConfig>
    {
        private readonly IUIFactory _uiFactory;
        private readonly IAllyPurchaseSystem _allyPurchaseSystem;

        public BuyUnitTutorial(IUIFactory uiFactory, IAllyPurchaseSystem allyPurchaseSystem)
        {
            _allyPurchaseSystem = allyPurchaseSystem;
            _uiFactory = uiFactory;
        }
        
        protected override void Show(BuyUnitTutorialConfig config)
        {
            BuyAllyTutorialWindow window = _uiFactory.CreateWindow(config.TutorialWindow) as BuyAllyTutorialWindow;
            window.BuyAlly.onClick.AddListener(CompleteTutorial);
        }

        private void CompleteTutorial()
        {
            _allyPurchaseSystem.TryPurchase(AllyType.Archer);
            IsCompleted = true;
            
        }
    }
}