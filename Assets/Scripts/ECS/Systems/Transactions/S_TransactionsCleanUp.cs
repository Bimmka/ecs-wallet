using ECS.Components.Transactions;
using Unity.Collections;
using Unity.Entities;

namespace ECS.Systems.Transactions
{
    public partial class S_TransactionsCleanUp : SystemBase
    {
        protected override void OnUpdate()
        {
            EntityCommandBuffer buffer = new EntityCommandBuffer(Allocator.Temp);
            foreach ((var c_transactions, var entity) in SystemAPI.Query<C_Transactions>().WithEntityAccess())
            {
                buffer.SetComponentEnabled<C_Transactions>(entity, false);
            }
            
            buffer.Playback(EntityManager);
        }
    }
}