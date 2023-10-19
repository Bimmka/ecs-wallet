using StateMachines.ApplicationGameStates.States.Interfaces;
using UI.Factory;

namespace StateMachines.ApplicationGameStates.States
{
    public class GameLevelInitializeState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IUIFactory _uiFactory;

        public GameLevelInitializeState(IGameStateMachine gameStateMachine, IUIFactory uiFactory)
        {
            _gameStateMachine = gameStateMachine;
            _uiFactory = uiFactory;
        }

        public void Enter()
        {
            _uiFactory.CreateWalletDisplayer();
            _gameStateMachine.Enter<EntitiesSystemsInitializeState>();
        }

        public void Exit()
        {
            
        }
    }
}