﻿namespace StateMachines
{
  public abstract class BaseStateMachineState
  {
    protected int animationName;

    //public abstract bool IsCanBeInterrupted();

    public virtual void Enter() =>
      Check();

    public virtual void Check() {}

    public virtual void PhysicsUpdate() =>
      Check();

    public virtual void Update(float deltaTime) {}

    public virtual void Exit() { }

    public virtual void TriggerAnimation() { }
  }
}