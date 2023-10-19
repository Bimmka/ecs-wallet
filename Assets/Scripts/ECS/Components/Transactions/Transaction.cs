using Currencies;

namespace ECS.Components.Transactions
{
    public struct Transaction
    {
        public TransactionType TransactionType;
        public CurrencyType CurrencyType;
        public int Amount;
    }
}