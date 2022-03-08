using System;
using UnityEngine;

namespace Core
{
    public class MonoEventsBroadcaster : MonoBehaviour
    {
        private static MonoEventsBroadcaster _monoEventsBroadcasterInstance;
        private bool _quit;
        private bool _created;
        
        public event Action<bool> ApplicationPauseChanged = b => { };
        public event Action<bool> ApplicationFocusChanged = b => { };
        public event Action ApplicationQuit = () => { };
        public event Action OnUpdate = () => { };
        public event Action OnLateUpdate = () => { };
        public event Action OnFixedUpdate = () => { };

        private void Awake()
        {
            if (_monoEventsBroadcasterInstance)
            {
                Destroy(this);
                return;
            }

            _created = true;
            _monoEventsBroadcasterInstance = this;
            Application.quitting += OnApplicationQuitting;
        }

        private void Update()
        {
            OnUpdate();
        }

        private void LateUpdate()
        {
            OnLateUpdate();
        }

        private void FixedUpdate()
        {
            OnFixedUpdate();
        }

        private void OnApplicationQuitting()
        {
            if (_quit)
            {
                return;
            }

            _quit = true;
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
            if (!_created)
            {
                return;
            }
            
            Application.quitting -= OnApplicationQuitting;
            
            if (!_quit)
            {
                OnApplicationQuitting();
            }
        }
    }
}