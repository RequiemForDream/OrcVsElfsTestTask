using Zenject;

namespace CodeBase.Gameplay.Allies.Purchase
{
    public interface IPriceProvider
    {
        int GetPrice();
        void UpdatePurchasesCount();
    }
}