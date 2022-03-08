using UnityEngine;

namespace GameScripts.UI
{
    public class PlayerPreferences
    {
        private const string LEVEL_INDEX = "level_index";

        public int LevelIndex
        {
            get => PlayerPrefs.GetInt(LEVEL_INDEX, 0);
            set => PlayerPrefs.SetInt(LEVEL_INDEX, value);
        }
    }
}