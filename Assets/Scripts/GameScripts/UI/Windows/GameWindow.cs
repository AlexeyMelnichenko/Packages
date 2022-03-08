using Core.UI;

namespace GameScripts.UI.Windows
{
    public class GameWindow : WindowWithIntent<GameIntent>
    {
        protected override void OnOpening()
        {
        }

        protected override void OnClosing()
        {
        }
    }

    public class GameIntent : EmptyIntent
    {
        public readonly Game Game;

        public GameIntent(Game game)
        {
            Game = game;
        }
    }
}