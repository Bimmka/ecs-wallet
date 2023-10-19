using Transactions;
using Unity.Entities;

namespace ECS.Components.Transactions
{
    public class C_TransactionsHandlerContainer : IComponentData
    {
        public TransactionsHandler Handler;
    }
}