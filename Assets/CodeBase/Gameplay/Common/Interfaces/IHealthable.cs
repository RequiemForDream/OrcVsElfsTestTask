namespace CodeBase.Gameplay.Common.Interfaces
{
    public interface IHealthable
    {
        bool IsAlive { get; }
        float Health { get; }
    }
}