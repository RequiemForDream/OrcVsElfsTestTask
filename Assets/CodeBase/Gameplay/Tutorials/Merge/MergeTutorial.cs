using CodeBase.Gameplay.AllySpawn;
using CodeBase.Gameplay.Levels;
using CodeBase.Gameplay.Merge;
using CodeBase.Gameplay.Pause;
using CodeBase.Gameplay.UI.Factory;
using UnityEngine;

namespace CodeBase.Gameplay.Tutorials.Merge
{
    public class MergeTutorial : Tutorial<MergeTutorialConfig>
    {
        private readonly IMergeService _mergeService;
        private readonly IPauseService _pauseService;
        private readonly ILevelDataProvider _levelDataProvider;
        private readonly IUIFactory _uiFactory;
        
        private MergeTutorialWindow _window;
        private readonly IAllySpawnSystem _allySpawnSystem;

        public MergeTutorial(IMergeService mergeService, IPauseService pauseService, ILevelDataProvider levelDataProvider,
            IUIFactory uiFactory, IAllySpawnSystem allySpawnSystem)
        {
            _allySpawnSystem = allySpawnSystem;
            _uiFactory = uiFactory;
            _levelDataProvider = levelDataProvider;
            _pauseService = pauseService;
            _mergeService = mergeService;
        }
        
        protected override void Show(MergeTutorialConfig config)
        {
            _window = _uiFactory.CreateWindow(config.WindowID) as MergeTutorialWindow;
            _window.MergeTutorialPointer.SetCamera(_levelDataProvider.MainCamera);
            _window.MergeTutorialPointer.PlayDragLoop(_allySpawnSystem.Allies[0].Transform, _allySpawnSystem.Allies[1].Transform);
            _mergeService.OnMerged += CompleteTutorial;
        }

        private void CompleteTutorial()
        {
            Object.Destroy(_window.gameObject);
            _mergeService.OnMerged -= CompleteTutorial;
            IsCompleted = true;
            _pauseService.Resume();
        }
    }
}