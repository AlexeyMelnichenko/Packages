using System.Threading.Tasks;
using Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.UI.Windows
{
    public class SettingsWindow : WindowWithIntent<SettingsIntent>
    {
        [SerializeField] private WindowAnimationController _windowAnimationController;
        [SerializeField] private AnimatedToggle _soundsToggle;
        [SerializeField] private AnimatedToggle _musicToggle;
        [SerializeField] private AnimatedToggle _vibroToggle;
        [SerializeField] private Button _closeButton;

        public Settings Settings => Intent.Settings;

        private void Awake()
        {
            _soundsToggle.ToggleValueChanged += OnSoundToggleClick;
            _musicToggle.ToggleValueChanged += OnMusicToggleClick;
            _vibroToggle.ToggleValueChanged += OnVibroToggleClick;
            _closeButton.onClick.AddListener(OnCloseClick);
        }

        protected override void OnOpening()
        {
            _soundsToggle.SetValueWithoutNotification(Settings.SoundsEnabled);
            _musicToggle.SetValueWithoutNotification(Settings.MusicEnabled);
            _vibroToggle.SetValueWithoutNotification(Settings.VibroEnabled);
        }

        protected override Task AnimateOpening()
        {
            return _windowAnimationController.OpenAnimation();
        }

        protected override void OnClosing()
        {
        }

        protected override Task AnimateClosing()
        {
            return _windowAnimationController.CloseAnimation();
        }

        private void OnSoundToggleClick(bool isOn)
        {
            Settings.SoundsEnabled = isOn;
        }

        private void OnMusicToggleClick(bool isOn)
        {
            Settings.MusicEnabled = isOn;
        }

        private void OnVibroToggleClick(bool isOn)
        {
            Settings.VibroEnabled = isOn;
        }

        private void OnCloseClick()
        {
            Close();
        }
    }

    public class SettingsIntent : EmptyIntent
    {
        public readonly Settings Settings;
        public readonly Game Game;

        public SettingsIntent(Settings settings, Game game)
        {
            Settings = settings;
            Game = game;
        }
    }
}
