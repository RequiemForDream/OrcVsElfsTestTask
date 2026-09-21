using System;

namespace CodeBase.Gameplay.Common.Health
{
    public interface IHealth
    {
        float CurrentHealth { get; }
        event Action<float> OnHealthChanged;
        void ApplyDamage(float damageAmount);
    }
}