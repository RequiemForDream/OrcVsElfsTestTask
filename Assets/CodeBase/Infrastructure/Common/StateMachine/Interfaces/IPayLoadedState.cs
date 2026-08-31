namespace CodeBase.Infrastructure.Common.StateMachine.Interfaces
{
    public interface IPayLoadedState<in TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}