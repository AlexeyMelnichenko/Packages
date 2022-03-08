using System.Threading.Tasks;
using Core.UI;

namespace GameScripts.UI.Windows
{
    public class SplashWindow : WindowWithIntent<SplashIntent>
    {
        private Task _delayTask;
        
        protected override void OnOpening()
        {
            _delayTask = Task.Delay(5000);
        }

        protected override async void OnOpened()
        {
            await Task.WhenAny(_delayTask, Intent.RemoteConfigInitTask);
            
            Close();
        }
    }

    public class SplashIntent : EmptyIntent
    {
        public Task RemoteConfigInitTask { get; }

        public SplashIntent(Task remoteConfigInitTask)
        {
            RemoteConfigInitTask = remoteConfigInitTask;
        }
    }
}