using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Currency
{
    public class CurrencyCounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currencyCounterText;
        [SerializeField] private float _countDuration = 0.5f;
        [SerializeField] private Ease _countEase = Ease.OutCubic;

        private ICurrencyCounter _currencyCounter;
        private Tween _countTween;
        private int _displayedValue;

        [Inject]
        private void Construct(ICurrencyCounter currencyCounter)
        {
            _currencyCounter = currencyCounter;
        }

        public void Initialize()
        {
            _currencyCounter.OnValueChanged += OnCurrencyChanged;

            _displayedValue = _currencyCounter.CurrentValue;
            UpdateText(_displayedValue);
        }

        private void OnDestroy()
        {
            _currencyCounter.OnValueChanged -= OnCurrencyChanged;
            _countTween?.Kill();
        }

        private void OnCurrencyChanged(int newValue)
        {
            _countTween?.Kill();

            _countTween = DOTween.To(
                () => _displayedValue,
                value =>
                {
                    _displayedValue = value;
                    UpdateText(value);
                },
                newValue,
                _countDuration
            ).SetEase(_countEase);
        }

        private void UpdateText(int value)
        {
            _currencyCounterText.text = value.ToString();
        }
    }
}