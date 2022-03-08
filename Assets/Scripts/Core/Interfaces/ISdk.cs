using Core.SDK;

namespace Core.Interfaces
{
    public interface ISdk
    {
        bool Initialized { get; }
        void Initialize();
        RemoteConfig RemoteConfig { get; }
    }
}