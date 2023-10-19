using Unity.Entities;

namespace ECS.Groups
{
    [UpdateAfter(typeof(GameLoopSaveGroup))]
    public partial class GameLoopCleanUpGroup : ComponentSystemGroup
    {
        
    }
}