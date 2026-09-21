using System;
using CodeBase.Gameplay.Tutorials;

namespace CodeBase.Gameplay.Currency
{
    public class CurrencyCounter : ICurrencyCounter
    {
        public event Action<int> OnValueChanged;

        public int CurrentValue
        {
            get => _currency;
            private set => _currency = value;
        }
        
        private int _currency;
        
        private readonly ITutorialsService _tutorialsService;

        public CurrencyCounter(ITutorialsService tutorialsService)
        {
            _tutorialsService = tutorialsService;
        }
        
        public void Add(int value)
        {
            CurrentValue += value;
            if (CurrentValue >= 10)
            {
                _tutorialsService.ShowTutorialByType(TutorialType.BuyAllyTutorial);
            }
            
            OnValueChanged?.Invoke(CurrentValue);
        }

        public void Spend(int value)
        {
            CurrentValue -= value;
            OnValueChanged?.Invoke(CurrentValue);
        }
    }
}