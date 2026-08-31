namespace CodeBase.Infrastructure.Common.StateMachine.Interfaces
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}