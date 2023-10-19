using ECS.Components.SaveLoad;
using SaveLoad;
using Unity.Entities;

namespace ECS.Systems.SaveLoad
{
    public partial class S_SaveLoadBuild : SystemBase
    {
        private ISaveLoadService _saveLoadService;

        public void Construct(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }
        
        protected override void OnUpdate()
        {
            var e_saveLoad = EntityManager.CreateSingleton(new C_SaveLoadContainer() { SaveLoadService = _saveLoadService });
            Enabled = false;
        }
    }
}