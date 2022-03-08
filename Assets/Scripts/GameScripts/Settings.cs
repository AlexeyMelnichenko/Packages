using System;

namespace GameScripts
{
    public class Settings
    {
        private readonly GamePreferences _preferences;
        public event Action<bool> MusicEnabledChanged; 
        public event Action<bool> SoundsEnabledChanged; 
        public event Action<bool> VibroEnabledChanged; 

        public bool MusicEnabled
        {
            get => _preferences.MusicEnabled;
            set
            {
                if (_preferences.MusicEnabled != value)
                {
                    _preferences.MusicEnabled = value;
                    MusicEnabledChanged?.Invoke(value);
                }
            }
        }

        public bool SoundsEnabled
        {
            get => _preferences.SoundsEnabled;
            set
            {
                if (_preferences.SoundsEnabled != value)
                {
                    _preferences.SoundsEnabled = value;
                    SoundsEnabledChanged?.Invoke(value);
                }
            }
        }
        
        public bool VibroEnabled
        {
            get => _preferences.VibroEnabled;
            set
            {
                if (_preferences.VibroEnabled != value)
                {
                    _preferences.VibroEnabled = value;
                    VibroEnabledChanged?.Invoke(value);
                }
            }
        }

        public Settings(GamePreferences preferences)
        {
            _preferences = preferences;
        }
    }
}