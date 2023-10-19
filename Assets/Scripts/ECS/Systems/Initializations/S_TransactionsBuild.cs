using System.Collections.Generic;
using ECS.Components.Transactions;
using Transactions;
using Unity.Entities;

namespace ECS.Systems.Initializations
{
    public partial class S_TransactionsBuild : SystemBase
    {
        private TransactionsHandler _transactionsHandler;
        public void Construct(TransactionsHandler transactionsHandler)
        {
            _transactionsHandler = transactionsHandler;
        }

        protected override void OnUpdate()
        {
            var e_transactionsHandler = EntityManager.CreateEntity();
            EntityManager.AddComponent<C_TransactionsHandlerTag>(e_transactionsHandler);
            C_TransactionsHandlerContainer c_container = new C_TransactionsHandlerContainer() { Handler = _transactionsHandler };
            EntityManager.AddComponentData(e_transactionsHandler, c_container);
            EntityManager.AddComponentData(e_transactionsHandler, new C_Transactions() { Transactions = new List<Transaction>() });
            EntityManager.SetComponentEnabled<C_Transactions>(e_transactionsHandler, false);
            _transactionsHandler.SetWorld(World, e_transactionsHandler);
            Enabled = false;
        }
    }
}