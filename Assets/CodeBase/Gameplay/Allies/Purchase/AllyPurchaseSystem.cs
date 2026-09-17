using System;
using CodeBase.Gameplay.AllySpawn;
using CodeBase.Gameplay.Currency;

namespace CodeBase.Gameplay.Allies.Purchase
{
    public class AllyPurchaseSystem : IAllyPurchaseSystem
    {
        public event Action OnMakePurchase;
        
        private readonly IAllySpawnSystem _spawnSystem;
        private readonly ICurrencyCounter _currencyCounter;
        private readonly IPriceProvider _priceProvider;

        public AllyPurchaseSystem(
            IAllySpawnSystem spawnSystem,
            ICurrencyCounter currencyCounter,
            IPriceProvider priceProvider)
        {
            _priceProvider = priceProvider;
            _spawnSystem = spawnSystem;
            _currencyCounter = currencyCounter;
        }

        public bool CanPurchase(AllyType type)
        {
            int price = _priceProvider.GetPrice();
            return _currencyCounter.CurrentValue >= price;
        }

        public bool TryPurchase(AllyType type)
        {
            if (!CanPurchase(type))
                return false;

            int price = _priceProvider.GetPrice();
            _currencyCounter.Spend(price); 
            _priceProvider.UpdatePurchasesCount();
            OnMakePurchase?.Invoke();
            _spawnSystem.Spawn(type);
            return true;
        }

        
    }
}