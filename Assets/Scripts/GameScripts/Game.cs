using Core.UI;
using UI;
using Zenject;

namespace GameScripts
{
    public class Game : IInitializable
    {
        private readonly WindowsController _windowsController;
        private GameWindow _gameWindow;

        public Game(WindowsController windowsController)
        {
            _windowsController = windowsController;
        }

        public void Initialize()
        {
            _gameWindow = _windowsController.Open<GameWindow>();
        }
    }
}