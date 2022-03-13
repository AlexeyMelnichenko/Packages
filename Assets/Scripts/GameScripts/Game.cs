using GameScripts.UI;

namespace GameScripts
{
    public class Game
    {
        public GamePreferences GamePreferences { get; }
        public PlayerPreferences PlayerPreferences { get; }
        public Settings Settings { get; }
        
        public Game()
        {
            GamePreferences = new GamePreferences();
            PlayerPreferences = new PlayerPreferences();
            Settings = new Settings(GamePreferences);
        }
    }
}