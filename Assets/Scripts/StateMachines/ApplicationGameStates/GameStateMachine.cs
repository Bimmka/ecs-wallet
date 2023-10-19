using System;
using System.Collections.Generic;
using StateMachines.ApplicationGameStates.States.Interfaces;
using StateMachines.Factory;
using Zenject;

namespace StateMachines.ApplicationGameStates
{
  public class GameStateMachine : IGameStateMachine
  {
    private readonly Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;
    private readonly GameStatesFactory _statesFactory;

    [Inject]
    public GameStateMachine(GameStatesFactory statesFactory)
    {
      _statesFactory = statesFactory;
      _states = new Dictionary<Type, IExitableState>(5);
    }

    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload);
    }

    public void Enter<TState, TPayload, TCallback>(TPayload payload, TCallback loadedCallback, TCallback curtainHideCallback) where TState : class, IPayloadedCallbackState<TPayload, TCallback>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload, loadedCallback, curtainHideCallback);
    }

    public TState GetState<TState>() where TState : class, IExitableState
    {
      if (_states.TryGetValue(typeof(TState), out IExitableState state))
        return (TState)state;

      TState createdState = _statesFactory.Create<TState>();
      _states.Add(typeof(TState), createdState);
      return createdState;
    }


    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();

      TState state = GetState<TState>();
      _activeState = state;

      return state;
    }
  }
}