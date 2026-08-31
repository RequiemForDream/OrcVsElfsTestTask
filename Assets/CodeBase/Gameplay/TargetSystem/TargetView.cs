using CodeBase.Gameplay.Common.Interfaces;
using UnityEngine;

namespace CodeBase.Gameplay.TargetSystem
{
    public abstract class TargetView : MonoBehaviour
    {
        public abstract ITarget Target { get; set; }
    }
}