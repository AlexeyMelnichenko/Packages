using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace Core.SDK
{
    public class RemoteConfig
    {
        private Dictionary<string, object> _values;
        private TaskCompletionSource<bool> _initializeTaskCompletionSource;
        
        public Task<bool> InitializeTask => _initializeTaskCompletionSource.Task;
        public bool Initialized { get; private set; }

        public void Init()
        {
            LoadLocalConfig();
        }

        private async void LoadLocalConfig()
        {
            _initializeTaskCompletionSource = new TaskCompletionSource<bool>();
            
            var defaultConfig = Resources.Load<TextAsset>("RemoteConfig");
            var defaultValues = defaultConfig != null
                ? JsonConvert.DeserializeObject<Dictionary<string, object>>(defaultConfig.text) 
                : new Dictionary<string, object>();
            
            //Emulation remote config fetching
            await Task.Delay(2000);

            _values = new Dictionary<string, object>(defaultValues);
            
            Initialized = true;
            _initializeTaskCompletionSource.SetResult(true);
        }

        public string GetString(string key, string defaultValue = "")
        {
            if (_values.ContainsKey(key))
            {
                try
                {
                    return (string)_values[key];
                }
                catch
                {
                    return defaultValue;
                }
            }

            return defaultValue;
        }

        public int GetInt(string key, int defaultValue = 0)
        {
            if (_values.ContainsKey(key))
            {
                try
                {
                    return Convert.ToInt32(_values[key]);
                }
                catch
                {
                    return defaultValue;
                }
            }

            return defaultValue;
        }
        
        public float GetFloat(string key, float defaultValue = 0f)
        {
            if (_values.ContainsKey(key))
            {
                try
                {
                    return Convert.ToSingle(_values[key]);
                }
                catch
                {
                    return defaultValue;
                }
            }

            return defaultValue;
        }

        public T GetObject<T>(string key, T defaultValue = default)
        {
            if (!_values.ContainsKey(key))
            {
                return defaultValue;
            }

            try
            {
                var value = _values[key];
                return value is Newtonsoft.Json.Linq.JToken token
                    ? token.ToObject<T>()
                    : Newtonsoft.Json.Linq.JToken.FromObject(value).ToObject<T>();
            }
            catch
            {
                return defaultValue;
            }
        }

        public Color GetColor(string key, Color defaultColor = default)
        {
            if (!_values.ContainsKey(key))
            {
                return defaultColor;
            }

            var colorString = _values[key] as string;

            if (string.IsNullOrEmpty(colorString))
            {
                return defaultColor;
            }

            if (ColorUtility.TryParseHtmlString(colorString, out var color))
            {
                return color;
            }
            
            return defaultColor;
        }
    }
}