using UnityEngine;

public abstract class BaseState
{
    protected MovementSM stateMachine;

    public virtual void Init(MovementSM stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public abstract void Enter();
    public abstract void UpdateLogic();
    public abstract void UpdatePhysics();
    public abstract void Exit();
}
