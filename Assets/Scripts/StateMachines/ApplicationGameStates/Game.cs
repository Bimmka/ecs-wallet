using StateMachines.ApplicationGameStates.States;

namespace StateMachines.ApplicationGameStates
{
  public class Game
  {
    public readonly IGameStateMachine StateMachine;

    public Game(IGameStateMachine gameStateMachine)
    {
        StateMachine = gameStateMachine;
    }

    public void StartGame()
    {
        StateMachine.Enter<GameLoadState>();
    }

    public void Cleanup()
    {
    }
    }
}