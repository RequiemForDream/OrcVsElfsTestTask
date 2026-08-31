using CodeBase.Gameplay.Allies.Configs;
using CodeBase.Gameplay.Arrows.Config;
using CodeBase.Gameplay.Board;
using CodeBase.Gameplay.Enemies.Configs;
using CodeBase.Gameplay.EnemySpawn;
using CodeBase.Gameplay.Tiles.Configs;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "GameConfigsInstaller", menuName = "Installers/GameConfigsInstaller")]
    public class GameConfigsInstaller : ScriptableObjectInstaller<GameConfigsInstaller>
    {
        public AllEnemiesConfigs allEnemiesConfigs;
        public AllAlliesConfigs AllAlliesConfigs;
        public BoardGenerationConfig boardGenerationConfig;
        public AllArrowsConfigs AllArrowsConfigs;
        public TileConfig TileConfig;
        public GameScenario GameScenario;
        
        public override void InstallBindings()
        {
            Container.BindInstances(allEnemiesConfigs, AllAlliesConfigs, boardGenerationConfig,  AllArrowsConfigs, TileConfig, GameScenario);
        }
    }
}