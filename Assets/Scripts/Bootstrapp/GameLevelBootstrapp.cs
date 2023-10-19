using StateMachines.ApplicationGameStates;
using StateMachines.ApplicationGameStates.States;
using StateMachines.Factory;
using Transactions;
using UI.Factory;
using Wallet;
using Zenject;

namespace Bootstrapp
{
    public class GameLevelBootstrapp : MonoInstaller
    {
        public override void Start()
        {
            Container.Resolve<Game>().StartGame();
        }

        public override void InstallBindings()
        {
            Container.Bind<Game>().ToSelf().FromNew().AsSingle();

            Container.Bind<IGameStateMachine>().To<GameStateMachine>().FromNew().AsSingle();
            Container.Bind<GameStatesFactory>().ToSelf().FromNew().AsSingle();
            Container.Bind<GameLoadState>().ToSelf().FromNew().AsSingle();
            Container.Bind<GameLevelInitializeState>().ToSelf().FromNew().AsSingle();
            Container.Bind<EntitiesSystemsInitializeState>().ToSelf().FromNew().AsSingle();
            Container.Bind<GameLoopState>().ToSelf().FromNew().AsSingle();

            Container.Bind<PlayerWallet>().ToSelf().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<TransactionsHandler>().FromNew().AsSingle();

            Container.Bind<IUIFactory>().To<UIFactory>().FromNew().AsSingle();
        }
    }
}