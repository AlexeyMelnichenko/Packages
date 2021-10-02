using Zenject;

namespace Core
{
    public class PlayerDataProvider : IInitializable
    {
        private readonly MonoEventsBroadcaster _monoEventsBroadcaster;
        
        public PlayerDataProvider(MonoEventsBroadcaster monoEventsBroadcaster)
        {
            _monoEventsBroadcaster = monoEventsBroadcaster;
        }
        
        public void Initialize()
        {
            _monoEventsBroadcaster.ApplicationQuit += OnQuit;
            _monoEventsBroadcaster.ApplicationFocusChanged += OnApplicationFocus;
            _monoEventsBroadcaster.ApplicationFocusChanged += OnApplicationFocus;
        }


        private void SavePlayerData()
        {
           
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                SavePlayerData();
            }
        }

        private void OnQuit()
        {
            SavePlayerData();
            
            _monoEventsBroadcaster.ApplicationQuit -= OnQuit;
            _monoEventsBroadcaster.ApplicationFocusChanged -= OnApplicationFocus;
        }


        public void Dispose()
        {
            OnQuit();
        }
    }
}