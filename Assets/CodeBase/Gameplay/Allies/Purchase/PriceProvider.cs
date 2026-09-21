namespace CodeBase.Gameplay.Allies.Purchase
{
    public class PriceProvider : IPriceProvider
    {
        private readonly int _startPrice = 10;
        private readonly int _priceIncreaseAfterEachPurchase = 5;

        private int _purchasesCount;

        public void UpdatePrice() => _purchasesCount++;
        
        public int GetPrice() => _startPrice + _priceIncreaseAfterEachPurchase * _purchasesCount;
    }
}