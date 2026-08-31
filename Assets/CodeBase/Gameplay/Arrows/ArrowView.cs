using System;
using UnityEngine;

namespace CodeBase.Gameplay.Arrows
{
    public class ArrowView : MonoBehaviour
    {
        public event Action OnDestroyHandler;

        private void OnDestroy()
        {
            OnDestroyHandler?.Invoke();
        }
    }
}