using UI.Elements;

namespace UI.Factory
{
    public interface IUIFactory
    {
        WalletDisplayer CreateWalletDisplayer();
    }
}