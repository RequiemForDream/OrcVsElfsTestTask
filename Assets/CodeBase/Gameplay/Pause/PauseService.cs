using System.Collections.Generic;

namespace CodeBase.Gameplay.Pause
{
    public class PauseService : IPauseService
    {
        public bool IsPaused {get; private set;}
        
        private readonly List<IPauseListener> _listeners = new List<IPauseListener>(16);
        
        public void Pause()
        {
            foreach (IPauseListener listener in _listeners)
            {
                listener.OnPause();
            }
            
            IsPaused = true;
        }

        public void Resume()
        {
            foreach (IPauseListener listener in _listeners)
            {
                listener.OnResume();
            }
            
            IsPaused = false;
        }

        public void AddListener(IPauseListener listener) => _listeners.Add(listener);
        public void RemoveListener(IPauseListener listener) => _listeners.Remove(listener);
    }
}