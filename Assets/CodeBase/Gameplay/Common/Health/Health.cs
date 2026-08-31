using System;

namespace CodeBase.Gameplay.Common.Health
{
    public class Health : IHealth
    {
        public event Action<float> OnHealthChanged;
        
        private readonly float _maxMaxHealth;
        private readonly HealthBar _healthBar;
        private float _health;

        public Health(float maxHealth)
        {
            _maxMaxHealth = _health = maxHealth;
        }

        public Health(float maxHealth, HealthBar healthBar)
        {
            _maxMaxHealth = _health = maxHealth;
            _healthBar = healthBar;
        }

        public void ApplyDamage(float damageAmount)
        {
            _health -= damageAmount;
            _healthBar?.UpdateHealth(_maxMaxHealth, _health);
            OnHealthChanged?.Invoke(_health);
        }
    }
}