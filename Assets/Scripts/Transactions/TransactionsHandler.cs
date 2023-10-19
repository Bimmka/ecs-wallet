using System.Collections.Generic;
using Currencies;
using ECS.Components.Transactions;
using Unity.Entities;

namespace Transactions
{
    public class TransactionsHandler
    {
        private readonly List<Transaction> _newTransactions = new List<Transaction>();

        private World _world;
        private Entity e_transactionHandler;


        public void SetWorld(World world, Entity e_transactionHandler)
        {
            this.e_transactionHandler = e_transactionHandler;
            _world = world;
        }

        public void OnUpdate()
        {
            if (_newTransactions.Count > 0 && _world.EntityManager.IsComponentEnabled<C_Transactions>(e_transactionHandler) == false)
            {
                C_Transactions transactions = _world.EntityManager.GetComponentData<C_Transactions>(e_transactionHandler);
                transactions.Transactions.Clear();
                for (int i = 0; i < _newTransactions.Count; i++)
                {
                    transactions.Transactions.Add(_newTransactions[i]);
                }
                _world.EntityManager.SetComponentEnabled<C_Transactions>(e_transactionHandler, true);
                _newTransactions.Clear();
            }
        }

        public void AddTransaction(TransactionType type, CurrencyType currencyType, int amount)
        {
            _newTransactions.Add(new Transaction()
            {
                TransactionType = type,
                CurrencyType = currencyType,
                Amount = amount
            });
        }
    }
}