using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Common.StateMachine.Interfaces;

namespace CodeBase.Infrastructure.Common.StateMachine
{
    public abstract class BaseStateMachine : IStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;
        private IUpdatableState _updatableState;
        private IPhysicsUpdatableState _physicsUpdatableState;
        
        public Type ActiveStateType => _activeState?.GetType();
        
        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayLoadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }
        
        public void Enter(Type stateType)
        {
            IExitableState state = ChangeState(stateType);

            if (state is IState simpleState)
                simpleState.Enter();
        }

        public void AddState(Type type, IExitableState state)
        {
            _states ??= new Dictionary<Type, IExitableState>();
            _states[type] = state;
        }

        public void UpdateStatePhysics()
        {
            _physicsUpdatableState?.UpdatePhysics();
        }

        public void UpdateStateLogic()
        {
            _updatableState?.UpdateLogic();
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _updatableState = state as IUpdatableState;
            _physicsUpdatableState = state as IPhysicsUpdatableState;
            _activeState = state;

            return state;
        }
        
        private IExitableState ChangeState(Type type)
        {
            _activeState?.Exit();

            IExitableState state = _states[type];
            _updatableState = state as IUpdatableState;
            _physicsUpdatableState = state as IPhysicsUpdatableState;
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}