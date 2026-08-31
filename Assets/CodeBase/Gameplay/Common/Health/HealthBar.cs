using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Gameplay.Common.Health
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;

        public void UpdateHealth(float maxHealth, float currentHealth)
        {
            _healthBar.DOFillAmount(currentHealth / maxHealth, 0.1f).SetEase(Ease.OutQuart);
        }
    }
}