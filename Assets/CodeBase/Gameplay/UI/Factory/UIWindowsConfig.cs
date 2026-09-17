using AYellowpaper.SerializedCollections;
using CodeBase.Gameplay.UI.Windows;
using UnityEngine;

namespace CodeBase.Gameplay.UI.Factory
{
    [CreateAssetMenu(fileName = "UI Windows Config", menuName = "UI/Create UI Windows Config", order = 0)]
    public class UIWindowsConfig : ScriptableObject
    {
        public Hud HudCanvas;
        public Canvas WindowsCanvas;
        
        public SerializedDictionary<WindowID, WindowBase> Windows;
    }
}