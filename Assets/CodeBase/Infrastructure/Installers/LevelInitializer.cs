using CodeBase.Gameplay.Levels;
using CurvedPathGenerator;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class LevelInitializer : MonoBehaviour, IInitializable
    {
        public Camera MainCamera;
        public Transform FieldPosition;
        public PathGenerator EnemyWalkPath;
        
        private ILevelDataProvider _levelDataProvider;

        [Inject]
        private void Construct(ILevelDataProvider levelDataProvider)
        {
            _levelDataProvider = levelDataProvider;
        }
        
        public void Initialize()
        {
            _levelDataProvider.SetEnemyWalkPath(EnemyWalkPath);
            _levelDataProvider.SetMainCamera(MainCamera);
        }
    }
}