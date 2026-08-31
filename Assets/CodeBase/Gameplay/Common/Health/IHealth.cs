using System;

namespace CodeBase.Gameplay.Common.Health
{
    public interface IHealth
    {
        event Action<float> OnHealthChanged;
        void ApplyDamage(float damageAmount);
    }
}