using Unity.Entities;

namespace ECS.Groups
{
    [UpdateAfter(typeof(GameLoopGroup))]
    public partial class GameLoopSaveGroup : ComponentSystemGroup
    {
        
    }
}