namespace CodeBase.Gameplay.Tutorials
{
    public abstract class Tutorial
    {
        public bool IsCompleted;    
        public abstract void Show(TutorialConfig config);
    }

    public abstract class Tutorial<TConfig> : Tutorial where TConfig : TutorialConfig
    {
        public sealed override void Show(TutorialConfig config)
        {
            Show((TConfig)config);
        }

        protected abstract void Show(TConfig config);
    }
}