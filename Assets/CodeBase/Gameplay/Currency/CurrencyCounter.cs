using System;
using CodeBase.Gameplay.Tutorials;

namespace CodeBase.Gameplay.Currency
{
    public class CurrencyCounter : ICurrencyCounter
    {
        public event Action<int> OnChange;

        public int CurrentValue
        {
            get => _currency;
            private set => _currency = value;
        }
        
        private int _currency;
        
        private readonly ITutorialsController _tutorialsController;

        public CurrencyCounter(ITutorialsController tutorialsController)
        {
            _tutorialsController = tutorialsController;
        }
        
        public void Add(int value)
        {
            CurrentValue += value;
            if (CurrentValue >= 10)
            {
                _tutorialsController.ShowTutorialByType(TutorialType.BuyTutorial);
            }
            
            OnChange?.Invoke(CurrentValue);
        }

        public void Spend(int value)
        {
            CurrentValue -= value;
            OnChange?.Invoke(CurrentValue);
        }
    }
}