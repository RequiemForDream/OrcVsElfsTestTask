using Zenject;

namespace CodeBase.Gameplay.Tutorials
{
    public interface ITutorialsService : IInitializable
    {
        void ShowTutorialByType(TutorialType type);
    }
}