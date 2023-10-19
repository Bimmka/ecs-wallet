using StateMachines.ApplicationGameStates.States.Interfaces;

namespace StateMachines.ApplicationGameStates
{
  public interface IGameStateMachine
  {
      void Enter<TState>() where TState : class, IState;
  }
}