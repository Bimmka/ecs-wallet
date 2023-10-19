using ECS.Components.Transactions;
using Unity.Entities;

namespace ECS.Systems.Transactions
{
    public partial class S_TransactionsUpdate : SystemBase
    {
        protected override void OnUpdate()
        {
            foreach (var c_transactionsHandler in SystemAPI.Query<C_TransactionsHandlerContainer>().WithAll<C_TransactionsHandlerTag>())
            {
                c_transactionsHandler.Handler.OnUpdate();
            }
        }
    }
}