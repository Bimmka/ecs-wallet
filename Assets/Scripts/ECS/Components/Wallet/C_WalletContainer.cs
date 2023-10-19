using Unity.Entities;
using Wallet;

namespace ECS.Components.Wallet
{
    public class C_WalletContainer : IComponentData
    {
        public Entity Entity;
        public PlayerWallet Wallet;
    }
}