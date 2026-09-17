using Zenject;

namespace CodeBase.Gameplay.EnemySpawn
{
    public interface IEnemySpawnSystem : ITickable
    {
        void StartScenario();
    }
}