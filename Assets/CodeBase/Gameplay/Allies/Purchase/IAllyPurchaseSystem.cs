using System;

namespace CodeBase.Gameplay.Allies.Purchase
{
    public interface IAllyPurchaseSystem
    {
        public event Action OnMakePurchase;
        bool CanPurchase(AllyType type);
        bool TryPurchase(AllyType type);
    }
}