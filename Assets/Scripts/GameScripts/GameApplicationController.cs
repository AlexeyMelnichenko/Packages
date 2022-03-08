using Core;
using Core.Interfaces;
using Core.UI;
using GameScripts.UI.Windows;

namespace GameScripts
{
    public class GameApplicationController : ApplicationController
    {
        public Game Game { get; private set; }
        public IWindowsController WindowsController { get; }
        
        public GameApplicationController(ISdk sdk, IWindowsController windowsController) : base(sdk)
        {
            WindowsController = windowsController;
        }

        protected override async void OnInitialize()
        {
            var splashWindow = WindowsController.Open<SplashWindow>();

            await splashWindow.CloseTask;

            Game = new Game();
            
            var gameWindowIntent = new GameIntent(Game);
            WindowsController.Open<GameWindow, GameIntent>(gameWindowIntent);
        }
    }
}