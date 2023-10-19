using Unity.Entities;

namespace ECS.Groups
{
    [UpdateAfter(typeof(PreGameLoopGroup))]
    public partial class GameLoopGroup : ComponentSystemGroup
    {
        
    }
}