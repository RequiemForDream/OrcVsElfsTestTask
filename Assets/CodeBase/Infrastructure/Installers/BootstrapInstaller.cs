using CodeBase.Gameplay.Allies;
using CodeBase.Gameplay.Allies.Factory;
using CodeBase.Gameplay.AllySpawn;
using CodeBase.Gameplay.Arrows.Factory;
using CodeBase.Gameplay.Board;
using CodeBase.Gameplay.Common.Random;
using CodeBase.Gameplay.Common.Time;
using CodeBase.Gameplay.Enemies.Factory;
using CodeBase.Gameplay.EnemySpawn;
using CodeBase.Gameplay.Levels;
using CodeBase.Gameplay.Tiles.Factory;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            BindInfrastructureServices();
            BindFactories();
            BindCommonServices();
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
        }

        private void BindGameplayServices()
        {
            Container.Bind<ILevelDataProvider>().To<LevelDataProvider>().AsSingle();
            Container.Bind<IBoardSystem>().To<BoardSystem>().AsSingle();
            Container.Bind<IAllySpawnSystem>().To<AllySpawnSystem>().AsSingle();
            Container.Bind<IEnemySpawnSystem>().To<EnemySpawnSystem>().AsSingle().NonLazy();
        }

        private void BindCommonServices()
        {
            Container.Bind<ITimeService>().To<UnityTimeService>().AsSingle();
            Container.Bind<IRandomService>().To<UnityRandomService>().AsSingle();
        }

        public void Initialize()
        {
            Container.Resolve<IBoardSystem>().Generate();
            Container.Resolve<IAllySpawnSystem>().Spawn(AllyType.Archer);
            /*Container.Resolve<IEnemyFactory>().Create(Vector3.zero, EnemyType.Orc);
            Container.Resolve<IEnemyFactory>().Create(Vector3.zero, EnemyType.Orc);*/
        }
    }
}