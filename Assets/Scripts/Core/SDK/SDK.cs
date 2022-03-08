using Core.Interfaces;

namespace Core.SDK
{
    public class SDK : ISdk
    {
        public bool Initialized { get; private set; }

        private RemoteConfig _remoteConfig;
        public RemoteConfig RemoteConfig => _remoteConfig ??= new RemoteConfig();

        public void Initialize()
        {
            if (Initialized)
            {
                return;
            }

            RemoteConfig.Init();

            Initialized = true;
        }
    }
}