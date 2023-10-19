using Constants;
using UI.Elements;
using UnityEngine;
using Zenject;

namespace UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly DiContainer _container;

        public UIFactory(DiContainer container)
        {
            _container = container;
        }
        
        public WalletDisplayer CreateWalletDisplayer()
        {
            WalletDisplayer prefab = Resources.Load<WalletDisplayer>(GameConstants.PathToWalletDisplayerPrefab);
            WalletDisplayer displayer = _container.InstantiatePrefab(prefab).GetComponent<WalletDisplayer>();
            return displayer;
        }
    }
}