using Core.UI;
using UI;
using Zenject;

namespace GameScripts
{
    public class Game : IInitializable
    {
        private GameWindow _gameWindow;
        private readonly WindowsController _windowsController;

        public Game(WindowsController windowsController)
        {
            _windowsController = windowsController;
        }

        public void Initialize()
        {
            var gameWindowIntent = new GameIntent(this);
            _gameWindow = _windowsController.Open<GameWindow, GameIntent>(gameWindowIntent);
        }
    }
}