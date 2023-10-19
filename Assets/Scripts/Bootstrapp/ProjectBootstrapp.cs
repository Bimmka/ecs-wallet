using Constants;
using SaveLoad;
using UnityEngine.SceneManagement;
using Zenject;

namespace Bootstrapp
{
    public class ProjectBootstrapp : MonoInstaller
    {
        public override void Start()
        {
            SceneManager.LoadScene(GameConstants.GameSceneName);
        }

        public override void InstallBindings()
        {
            Container.Bind<ISaveLoadService>().To<FileSaveLoadService>().FromNew().AsSingle();
        }
    }
}
