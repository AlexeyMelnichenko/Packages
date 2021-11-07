using Core;
using Core.Interfaces;
using UI;

namespace GameScripts
{
    public class GameApplicationController : ApplicationController
    {
        public Game Game { get; private set; }
        
        public GameApplicationController(IWindowsController windowsController) : base(windowsController)
        {
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