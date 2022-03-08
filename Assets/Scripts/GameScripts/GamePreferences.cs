using Core.Preferences;
using UnityEngine;

namespace GameScripts
{
    public class GamePreferences
    {
        private const string SOUNDS_ENABLED_KEY = "SoundsEnableed";
        public bool SoundsEnabled
        {
            get => PreferencesEx.GetBool(SOUNDS_ENABLED_KEY);
            set => PreferencesEx.SetBool(SOUNDS_ENABLED_KEY, value);
        }
        
        private const string MUSIC_ENABLED_KEY = "MusicEnableed";
        public bool MusicEnabled
        {
            get => PreferencesEx.GetBool(MUSIC_ENABLED_KEY);
            set => PreferencesEx.SetBool(MUSIC_ENABLED_KEY, value);
        }

        private const string VIBRO_ENABLED_KEY = "VibroEnableed";
        public bool VibroEnabled
        {
            get => PreferencesEx.GetBool(VIBRO_ENABLED_KEY);
            set => PreferencesEx.SetBool(VIBRO_ENABLED_KEY, value);
        }
        
        public GamePreferences()
        {
            if (!PlayerPrefs.HasKey(SOUNDS_ENABLED_KEY))
            {
                SoundsEnabled = true;
            }
            
            if (!PlayerPrefs.HasKey(VIBRO_ENABLED_KEY))
            {
                VibroEnabled = true;
            }

            if (!PlayerPrefs.HasKey(MUSIC_ENABLED_KEY))
            {
                MusicEnabled = true;
            }
        }
    }
}