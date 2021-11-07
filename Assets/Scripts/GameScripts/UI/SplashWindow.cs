using System.Threading.Tasks;
using Core.SDK;
using Core.UI;

namespace UI
{
    public class SplashWindow : WindowBase
    {
        private Task delayTask;
        
        protected override void OnOpening()
        {
            delayTask = Task.Delay(5000);
        }

        protected override async void OnOpened()
        {
            await Task.WhenAny(delayTask, SDK.InitializeTask);
            
            Close();
        }
    }
}