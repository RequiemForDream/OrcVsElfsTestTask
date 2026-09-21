namespace CodeBase.Gameplay.Pause
{
    public interface IPauseService
    {
        void Pause();
        void Resume();
        void AddListener(IPauseListener listener);
        void RemoveListener(IPauseListener listener);
        bool IsPaused { get; }
    }
}