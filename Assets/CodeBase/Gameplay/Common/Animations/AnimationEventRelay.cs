using System;
using UnityEngine;

namespace CodeBase.Gameplay.Common.Animations
{
    public class AnimationEventRelay : MonoBehaviour
    {
        public event Action<EventType> OnAnimationEventInvoke;
        
        public void HandleAnimationEvent(EventType eventType)
        {
            OnAnimationEventInvoke?.Invoke(eventType);
        }
    }

    public enum EventType
    {
        Unknown = 0,
        OnOrcAttack = 1,
        OnElfAttack = 2,
    }
}