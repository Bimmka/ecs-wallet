using Constants;
using SaveLoad;
using StateMachines.ApplicationGameStates.States.Interfaces;
using Wallet;

namespace StateMachines.ApplicationGameStates.States
{
    public class GameLoadState : IState
    {
        private readonly ISaveLoadService _saveLoadService;

        private readonly IGameStateMachine _gameStateMachine;
        private readonly PlayerWallet _wallet;

        public GameLoadState(IGameStateMachine gameStateMachine, ISaveLoadService saveLoadService, PlayerWallet wallet)
        {
            _wallet = wallet;
            _gameStateMachine = gameStateMachine;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            _saveLoadService.Load(GameConstants.SaveKey, OnSuccessLoad, OnFailLoad);
        }

        public void Exit()
        {
        }

        private void OnSuccessLoad(string saveData)
        {
            _wallet.Restore(saveData);
            SetNextState();
        }

        private void OnFailLoad()
        {
            _wallet.InitializeDefaultValues();
            SetNextState();
        }

        private void SetNextState()
        {
            _gameStateMachine.Enter<GameLevelInitializeState>();
        }
    }
}