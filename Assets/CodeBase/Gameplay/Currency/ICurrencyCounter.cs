using System;

namespace CodeBase.Gameplay.Currency
{
    public interface ICurrencyCounter
    {
        event Action<int> OnValueChanged;
        int CurrentValue { get; }
        void Add(int value);
        void Spend(int value);
    }
}