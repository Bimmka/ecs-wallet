using Constants;
using ECS.Components.SaveLoad;
using ECS.Components.Wallet;
using Unity.Entities;

namespace ECS.Systems.Wallet
{
    public partial class S_WalletSave : SystemBase
    {
        protected override void OnUpdate()
        {
            C_SaveLoadContainer saveLoadContainer = SystemAPI.ManagedAPI.GetSingleton<C_SaveLoadContainer>();
            foreach ((var c_walletContainer, var e_wallet) in SystemAPI.Query<C_WalletContainer>().WithAll<C_WalletTag, C_WalletChangeTag>().WithEntityAccess())
            {
                if (saveLoadContainer.SaveLoadService.InProcess)
                    continue;

                EntityManager.SetComponentEnabled<C_WalletChangeTag>(e_wallet, false);
                saveLoadContainer.SaveLoadService.Save(GameConstants.SaveKey, c_walletContainer.Wallet.Serialize());
            }
        }
    }
}