using SaveLoad;
using Unity.Entities;

namespace ECS.Components.SaveLoad
{
    public class C_SaveLoadContainer : IComponentData
    {
        public ISaveLoadService SaveLoadService;
    }
}