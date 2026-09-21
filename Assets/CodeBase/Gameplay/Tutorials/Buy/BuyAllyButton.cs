using CodeBase.Gameplay.Allies;
using CodeBase.Gameplay.Allies.Purchase;
using CodeBase.Gameplay.Currency;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.Gameplay.Tutorials.Buy
{
    public class BuyAllyButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _priveView;
        
        private IAllyPurchaseSystem _allyPurchaseSystem;
        private IPriceProvider _priceProvider;
        private ICurrencyCounter _currencyCounter;

        [Inject]
        private void Construct(ICurrencyCounter currencyCounter, IPriceProvider priceProvider, IAllyPurchaseSystem allyPurchaseSystem)
        {
            _currencyCounter = currencyCounter;
            _priceProvider = priceProvider;
            _allyPurchaseSystem = allyPurchaseSystem;
        }

        public void Initialize()
        {
            _button.onClick.AddListener(BuyAlly);
            _allyPurchaseSystem.OnMakePurchase += UpdatePriceView;
            _currencyCounter.OnValueChanged += CheckCurrencyAmount;
        }

        private void UpdatePriceView()
        {
            gameObject.SetActive(true);
            _priveView.text = _priceProvider.GetPrice().ToString();
            CheckCurrencyAmount(_currencyCounter.CurrentValue); 
        }

        private void CheckCurrencyAmount(int currencyAmount) => _button.interactable = currencyAmount >= _priceProvider.GetPrice();

        private void OnDestroy()
        {
            _currencyCounter.OnValueChanged -= CheckCurrencyAmount;
            _allyPurchaseSystem.OnMakePurchase -= UpdatePriceView;
            _button.onClick.RemoveAllListeners();
        }

        private void BuyAlly() => _allyPurchaseSystem.TryPurchase(AllyType.Archer);
    }
}