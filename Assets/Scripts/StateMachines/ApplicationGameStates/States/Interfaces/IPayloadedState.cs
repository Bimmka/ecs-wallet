﻿namespace StateMachines.ApplicationGameStates.States.Interfaces
{
  public interface IPayloadedState<TPayload> : IExitableState
  {
    void Enter(TPayload payload);
  }
}