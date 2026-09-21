using CodeBase.Gameplay.Currency;
using CodeBase.Gameplay.Tutorials.Buy;
using UnityEngine;

namespace CodeBase.Gameplay.UI
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private BuyAllyButton _buyAllyButton;
        [SerializeField] private CurrencyCounterView _currencyCounterView;

        public void Initialize()
        {
            _buyAllyButton.Initialize();
            _currencyCounterView.Initialize();
        }
    }
}