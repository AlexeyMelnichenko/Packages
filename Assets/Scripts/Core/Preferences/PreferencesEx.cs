using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Core.Preferences
{
    public static class PreferencesEx
    {
        public static void SetObject<T>(string key, T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            PlayerPrefs.SetString(key, json);
        }

        public static T GetObject<T>(string key, T defaultObj = default)
        {
            try
            {
                var json = PlayerPrefs.GetString(key, string.Empty);
                var obj = JsonConvert.DeserializeObject<T>(json);
                return obj != null ? obj : defaultObj;
            }
            catch (Exception)
            {
                return defaultObj;
            }
        }

        public static void SetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        public static bool GetBool(string key)
        {
            return PlayerPrefs.GetInt(key) == 1;
        }
    }
}