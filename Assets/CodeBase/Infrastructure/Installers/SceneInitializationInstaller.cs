using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class SceneInitializationInstaller : MonoInstaller
    {
        public MonoBehaviour[] Initializers;
    
        public override void InstallBindings()
        {
            foreach (MonoBehaviour initializer in Initializers)
            {
                Container.BindInterfacesTo(initializer.GetType()).FromInstance(initializer).AsSingle();
            }
        }
    }
}