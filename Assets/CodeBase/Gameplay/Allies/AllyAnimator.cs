using System;
using CodeBase.Gameplay.Common;
using CodeBase.Gameplay.Common.Animations;
using UnityEngine;

namespace CodeBase.Gameplay.Allies
{
    public class AllyAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Die = Animator.StringToHash("Die");
        
        private readonly int _idleStateHash = Animator.StringToHash("Idle");
        private readonly int _attackStateHash = Animator.StringToHash("Attack");
        private readonly int _deathStateHash = Animator.StringToHash("Death");
        
        public Animator Animator;
        
        public AnimatorState State { get; private set; }
        
        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public void PlayDeath() => Animator.SetTrigger(Die);
        public void PlayAttack() => Animator.SetTrigger(Attack);

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash)
        {
            StateExited?.Invoke(StateFor(stateHash));
        }
        
        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            if (stateHash == _idleStateHash)
                state = AnimatorState.Idle;
            else if (stateHash == _attackStateHash)
                state = AnimatorState.Attack;
            else if (stateHash == _deathStateHash)
                state = AnimatorState.Died;
            else
                state = AnimatorState.Unknown;
      
            return state;
        }
    }
}