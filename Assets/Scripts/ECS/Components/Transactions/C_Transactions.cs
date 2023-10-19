using System.Collections.Generic;
using Unity.Entities;

namespace ECS.Components.Transactions
{
    public class C_Transactions : IComponentData, IEnableableComponent
    {
        public List<Transaction> Transactions;
    }
}