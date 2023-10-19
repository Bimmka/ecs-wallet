using ECS.Components.Wallet;
using Unity.Entities;
using Wallet;

namespace ECS.Systems.Initializations
{
    public partial class S_WalletBuild : SystemBase
    {
        private PlayerWallet _playerWallet;

        public void Construct(PlayerWallet playerWallet)
        {
            _playerWallet = playerWallet;
        }
        
        protected override void OnUpdate()
        {
            var e_wallet = EntityManager.CreateSingleton(new C_WalletContainer() { Wallet = _playerWallet });
            EntityManager.GetComponentData<C_WalletContainer>(e_wallet).Entity = e_wallet;
            EntityManager.AddComponent<C_WalletTag>(e_wallet);
            EntityManager.AddComponent<C_WalletChangeTag>(e_wallet);
            EntityManager.SetComponentEnabled<C_WalletChangeTag>(e_wallet, false);
            Enabled = false;
        }
    }
}