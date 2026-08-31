namespace CodeBase.Infrastructure.Common.StateMachine.Interfaces
{
    public interface IStateMachine
    {
        void Enter<TState>() where TState : class, IState;
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayLoadedState<TPayload>;
    }
}