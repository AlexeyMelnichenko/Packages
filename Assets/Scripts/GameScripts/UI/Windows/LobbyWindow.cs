using Core.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.UI.Windows
{
    public class LobbyWindow : WindowWithIntent<LobbyIntent>
    {
        [SerializeField] private Button _playGameButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private TextMeshProUGUI _levelNum;
            
        private Game Game => Intent.Game;

        protected override void OnOpening()
        {
            _playGameButton.onClick.AddListener(OnPlayButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);

            UpdateLevelNumber(Game.PlayerPreferences.LevelIndex);
        }

        private void UpdateLevelNumber(int levelNum)
        {
            _levelNum.text = "Level " + levelNum;
        }

        protected override void OnClosing()
        {
            _playGameButton.onClick.RemoveListener(OnPlayButtonClick);
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClick);
        }

        private async void OnPlayButtonClick()
        {
        }

        private void OnSettingsButtonClick()
        {
            WindowsController.Open<SettingsWindow, SettingsIntent>(new SettingsIntent(Game.Settings, Game));
        }
    }

    public class LobbyIntent : EmptyIntent
    {
        public readonly Game Game;

        public LobbyIntent(Game game)
        {
            Game = game;
        }
    }
}