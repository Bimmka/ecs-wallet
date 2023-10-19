using ECS.Components.Transactions;
using ECS.Components.Wallet;
using SaveLoad;
using Unity.Entities;
using UnityEngine;

namespace ECS.Systems.Transactions
{
    [UpdateAfter(typeof(S_TransactionsUpdate))]
    public partial class S_MakeTransaction : SystemBase
    {
        protected override void OnUpdate()
        {
            C_WalletContainer c_walletContainer = SystemAPI.ManagedAPI.GetSingleton<C_WalletContainer>();

            foreach ( var c_transactions in SystemAPI.Query<C_Transactions>())
            {
                for (int i = 0; i < c_transactions.Transactions.Count; i++)
                {
                    switch (c_transactions.Transactions[i].TransactionType)
                    {
                        case TransactionType.Reset:
                            c_walletContainer.Wallet.InitializeDefaultValues();
                            break;
                        case TransactionType.Add:
                            c_walletContainer.Wallet.Add(c_transactions.Transactions[i].CurrencyType, c_transactions.Transactions[i].Amount);
                            break;
                        case TransactionType.Decrease:
                            if (c_walletContainer.Wallet.IsEnough(c_transactions.Transactions[i].CurrencyType, c_transactions.Transactions[i].Amount))
                                c_walletContainer.Wallet.Decrease(c_transactions.Transactions[i].CurrencyType, c_transactions.Transactions[i].Amount);
                            else
                                Debug.Log($"Cannot decrease {c_transactions.Transactions[i].CurrencyType} in wallet because doesn't have {c_transactions.Transactions[i].Amount} amount");
                            break;
                    }
                }

                if (EntityManager.IsComponentEnabled<C_WalletChangeTag>(c_walletContainer.Entity) == false)
                    EntityManager.SetComponentEnabled<C_WalletChangeTag>(c_walletContainer.Entity, true);
            }
        }
    }
}