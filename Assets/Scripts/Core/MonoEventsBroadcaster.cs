using System;
using UnityEngine;

namespace Core
{
    public class MonoEventsBroadcaster : MonoBehaviour
    {
        public event Action<bool> ApplicationPauseChanged;
        public event Action<bool> ApplicationFocusChanged;
        public event Action ApplicationQuit;

        private void Awake()
        {
            Application.quitting += OnApplicationQuit;
        }

        private void OnApplicationQuit()
        {
            ApplicationQuit?.Invoke();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            ApplicationFocusChanged?.Invoke(hasFocus);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            ApplicationPauseChanged?.Invoke(pauseStatus);
        }

        private void OnDestroy()
        {
            Application.quitting -= OnApplicationQuit;
        }
    }
}