namespace CodeBase.Gameplay.Pause
{
    public interface IPauseListener
    {
        void OnPause();
        void OnResume();
    }
}