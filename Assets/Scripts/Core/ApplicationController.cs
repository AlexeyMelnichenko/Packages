using Core.UI;
using UI;
using UnityEngine;
using Zenject;

namespace Core
{
    public class ApplicationController : IInitializable
    {
        private readonly WindowsController _windowsController;
        
        public SDK.SDK SDK { get; } = new SDK.SDK();

        public ApplicationController(WindowsController windowsController)
        {
            _windowsController = windowsController;
        }
        
        public async void Initialize()
        {
            Application.targetFrameRate = 60;

            SDK.Initialize();

            var splashWindow = _windowsController.Open<SplashWindow>();

            await splashWindow.CloseTask;

            var game = new GameScripts.Game(_windowsController);
            game.Initialize();
        }
    }
}