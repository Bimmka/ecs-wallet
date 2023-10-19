using System;
using UnityEngine;

namespace SaveLoad
{
    public class PlayerPrefsSaveLoadService : ISaveLoadService
    {
        public bool InProcess { get; }

        public void Save(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public void Load(string key, Action<string> onSuccess, Action onFail = null)
        {
            if (PlayerPrefs.HasKey(key))
            {
                string data = PlayerPrefs.GetString(key, string.Empty);
                onSuccess?.Invoke(data);
            }
            else
                onFail?.Invoke();
        }
    }
}