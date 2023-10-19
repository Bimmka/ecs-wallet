using ECS.Groups;
using ECS.Systems.Initializations;
using ECS.Systems.SaveLoad;
using ECS.Systems.Transactions;
using ECS.Systems.Wallet;
using SaveLoad;
using StateMachines.ApplicationGameStates.States.Interfaces;
using Transactions;
using Unity.Entities;
using Wallet;

namespace StateMachines.ApplicationGameStates.States
{
    public class EntitiesSystemsInitializeState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly PlayerWallet _playerWallet;
        private readonly TransactionsHandler _transactionsHandler;
        private readonly ISaveLoadService _saveLoadService;

        public EntitiesSystemsInitializeState(IGameStateMachine gameStateMachine, PlayerWallet playerWallet, TransactionsHandler transactionsHandler,
            ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _transactionsHandler = transactionsHandler;
            _playerWallet = playerWallet;
            _gameStateMachine = gameStateMachine;
        }
        
        public void Enter()
        {
            var world = new World("PlayerWorld");

            var initializationSystemGroup = world.CreateSystemManaged<InitializationSystemGroup>();
            var simulationSystemGroup = world.CreateSystemManaged<SimulationSystemGroup>();

            var s_transactionsBuild = world.CreateSystemManaged<S_TransactionsBuild>();
            s_transactionsBuild.Construct(_transactionsHandler);
            initializationSystemGroup.AddSystemToUpdateList(s_transactionsBuild);

            var s_walletBuild = world.CreateSystemManaged<S_WalletBuild>();
            s_walletBuild.Construct(_playerWallet);
            initializationSystemGroup.AddSystemToUpdateList(s_walletBuild);
            
            var s_saveLoadBuild = world.CreateSystemManaged<S_SaveLoadBuild>();
            s_saveLoadBuild.Construct(_saveLoadService);
            initializationSystemGroup.AddSystemToUpdateList(s_saveLoadBuild);

            var preGameLoopGroup = world.CreateSystemManaged<PreGameLoopGroup>();
            var gameLoopGroup = world.CreateSystemManaged<GameLoopGroup>();
            var gameLoopSaveGroup = world.CreateSystemManaged<GameLoopSaveGroup>();
            var gameLoopCleanUpGroup = world.CreateSystemManaged<GameLoopCleanUpGroup>();

            simulationSystemGroup.AddSystemToUpdateList(preGameLoopGroup);
            simulationSystemGroup.AddSystemToUpdateList(gameLoopGroup);
            simulationSystemGroup.AddSystemToUpdateList(gameLoopSaveGroup);
            simulationSystemGroup.AddSystemToUpdateList(gameLoopCleanUpGroup);

            var s_updateTransaction = world.CreateSystem<S_TransactionsUpdate>();
            gameLoopGroup.AddSystemToUpdateList(s_updateTransaction);

            var s_makeTransaction = world.CreateSystem<S_MakeTransaction>();
            gameLoopGroup.AddSystemToUpdateList(s_makeTransaction);

            var s_transactionsCleanUp = world.CreateSystem<S_TransactionsCleanUp>();
            gameLoopCleanUpGroup.AddSystemToUpdateList(s_transactionsCleanUp);

            var s_walletSave = world.CreateSystem<S_WalletSave>();
            gameLoopSaveGroup.AddSystemToUpdateList(s_walletSave);
            

            ScriptBehaviourUpdateOrder.AppendWorldToCurrentPlayerLoop(world);
            _gameStateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
      
        }
    }
}