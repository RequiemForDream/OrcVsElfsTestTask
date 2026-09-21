using CodeBase.Gameplay.Allies;
using CodeBase.Gameplay.Allies.Purchase;
using CodeBase.Gameplay.Pause;
using CodeBase.Gameplay.UI.Factory;
using UnityEngine;

namespace CodeBase.Gameplay.Tutorials.Buy
{
    public class BuyAllyTutorial : Tutorial<BuyAllyTutorialConfig>
    {
        private readonly IUIFactory _uiFactory;
        private readonly IAllyPurchaseSystem _allyPurchaseSystem;
        private readonly IPauseService _pauseService;
        private readonly ITutorialsService _tutorialsService;
        
        private BuyAllyTutorialWindow _window;

        public BuyAllyTutorial(IUIFactory uiFactory, IAllyPurchaseSystem allyPurchaseSystem, IPauseService pauseService,
            ITutorialsService tutorialsService)
        {
            _tutorialsService = tutorialsService;
            _pauseService = pauseService;
            _allyPurchaseSystem = allyPurchaseSystem;
            _uiFactory = uiFactory;
        }
        
        protected override void Show(BuyAllyTutorialConfig config)
        {
            _pauseService.Pause();
            _window = _uiFactory.CreateWindow(config.TutorialWindow) as BuyAllyTutorialWindow;
            _window.BuyAlly.onClick.AddListener(CompleteTutorial);
        }

        private void CompleteTutorial()
        {
            _allyPurchaseSystem.TryPurchase(AllyType.Archer);
            Object.Destroy(_window.gameObject);
            IsCompleted = true;
            _tutorialsService.ShowTutorialByType(TutorialType.MergeTutorial);
        }
    }
}