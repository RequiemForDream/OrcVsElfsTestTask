using Zenject;

namespace CodeBase.Gameplay.Tutorials
{
    public interface ITutorialsController : IInitializable
    {
        void ShowTutorialByType(TutorialType type);
    }
}