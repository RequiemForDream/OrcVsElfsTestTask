using CodeBase.Gameplay.UI.Windows;
using UnityEngine;

namespace CodeBase.Gameplay.UI.Factory
{
    public interface IUIFactory
    {
        Hud CreateMainHud();
        Canvas CreateWindowsHud();
        WindowBase CreateWindow(WindowID windowID);
    }
}