using CodeBase.Gameplay.Allies;
using CodeBase.Gameplay.Allies.Factory;
using CodeBase.Gameplay.Allies.Purchase;
using CodeBase.Gameplay.AllySpawn;
using CodeBase.Gameplay.Arrows.Factory;
using CodeBase.Gameplay.Board;
using CodeBase.Gameplay.Common.Random;
using CodeBase.Gameplay.Common.Time;
using CodeBase.Gameplay.Currency;
using CodeBase.Gameplay.Enemies.Factory;
using CodeBase.Gameplay.EnemySpawn;
using CodeBase.Gameplay.Levels;
using CodeBase.Gameplay.Merge;
using CodeBase.Gameplay.Pause;
using CodeBase.Gameplay.Tiles.Factory;
using CodeBase.Gameplay.Tutorials;
using CodeBase.Gameplay.UI.Factory;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            BindInfrastructureServices();
            BindCommonServices();
            BindFactories();
            BindGameplayServices();
        }

        private void BindInfrastructureServices()
        {
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
        }

        private void BindFactories()
        {
            Container.Bind<ITileFactory>().To<TileFactory>().AsSingle();
            Container.Bind<IArrowFactory>().To<ArrowFactory>().AsSingle();
            Container.Bind<IAllyFactory>().To<AllyFactory>().AsSingle();
            Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsSingle();
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
        }

        private void BindGameplayServices()
        {
            Container.Bind<IPauseService>().To<PauseService>().AsSingle();
            Container.Bind<IMergeService>().To<MergeService>().AsSingle();
            Container.BindInterfacesAndSelfTo<TutorialsService>().AsSingle();
            Container.Bind<ICurrencyCounter>().To<CurrencyCounter>().AsSingle();
            Container.BindInterfacesAndSelfTo<PriceProvider>().AsSingle();
            Container.Bind<IAllyPurchaseSystem>().To<AllyPurchaseSystem>().AsSingle();
            Container.Bind<ILevelDataProvider>().To<LevelDataProvider>().AsSingle();
            Container.Bind<IBoardSystem>().To<BoardSystem>().AsSingle();
            Container.Bind<IAllySpawnSystem>().To<AllySpawnSystem>().AsSingle();
            Container.Bind<IEnemySpawnSystem>().To<EnemySpawnSystem>().AsSingle();
        }

        private void BindCommonServices()
        {
            Container.Bind<ITimeService>().To<UnityTimeService>().AsSingle();
            Container.Bind<IRandomService>().To<UnityRandomService>().AsSingle();
        }

        public void Initialize()
        {
            Container.Resolve<IUIFactory>().CreateMainHud();
            Container.Resolve<IBoardSystem>().Generate();
            Container.Resolve<IAllySpawnSystem>().Spawn(AllyType.Archer);
            Container.Resolve<IEnemySpawnSystem>().StartScenario();
        }
    }
}