using System;

public abstract class State
{
    public virtual void Enter() {}
    public virtual void Update(float deltaTime) {}
    public virtual void Update() {}
    public virtual void Exit() {}
}
