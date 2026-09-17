using CodeBase.Gameplay.UI.Windows;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly DiContainer _diContainer;
        private readonly UIWindowsConfig _windowsConfig;

        private Canvas _windowsHud;

        public UIFactory(DiContainer diContainer, UIWindowsConfig windowsConfig)
        {
            _windowsConfig = windowsConfig;
            _diContainer = diContainer;
        }
        
        public Hud CreateMainHud()
        {
            Hud mainHud = _diContainer.InstantiatePrefabForComponent<Hud>(_windowsConfig.HudCanvas);
             mainHud.Initialize();
            return mainHud;
        }

        public Canvas CreateWindowsHud() => Object.Instantiate(_windowsConfig.WindowsCanvas);

        public WindowBase CreateWindow(WindowID windowID)
        {
            if (_windowsHud == null)
            {
                _windowsHud = CreateWindowsHud();
            }
            WindowBase window = _windowsConfig.Windows[windowID];
            return _diContainer.InstantiatePrefabForComponent<WindowBase>(window, _windowsHud.transform);
        }
    }
}