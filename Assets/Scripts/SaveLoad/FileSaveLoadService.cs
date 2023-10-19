using System;
using System.IO;
using Constants;

namespace SaveLoad
{
    public class FileSaveLoadService : ISaveLoadService
    {
        public bool InProcess { get; private set; }

        public async void Save(string key, string value)
        {
            InProcess = true;
            CheckForFileExists();

            try
            {
                using (StreamWriter outputFile = new StreamWriter(GameConstants.PathToSaveFile))
                {
                    await outputFile.WriteAsync(value);
                }

                InProcess = false;
            }
            catch (Exception e)
            {
                InProcess = false;
                Console.WriteLine(e);
                throw;
            }
        }

        public async void Load(string key, Action<string> onSuccess, Action onFail = null)
        {
            InProcess = true;
            CheckForFileExists();

            try
            {
                string saveData;
                using (StreamReader stream = new StreamReader(GameConstants.PathToSaveFile))
                {
                    saveData = await stream.ReadToEndAsync();
                }
                
                InProcess = false;
                onSuccess?.Invoke(saveData);
            }
            catch (Exception e)
            {
                InProcess = false;
                onFail?.Invoke();
                Console.WriteLine(e);
                throw;
            }
        }

        private static void CheckForFileExists()
        {
            if (File.Exists(GameConstants.PathToSaveFile) == false)
            {
                if (Directory.Exists(GameConstants.PathToSaveFolder) == false)
                    Directory.CreateDirectory(GameConstants.PathToSaveFolder);

                File.Create(GameConstants.PathToSaveFile);
            }
        }
    }
}