using System;

namespace SaveLoad
{
    public interface ISaveLoadService
    {
        bool InProcess { get; }
        void Save(string key, string value);
        void Load(string key, Action<string> onSuccess, Action onFail = null);
    }
}