using UnityEngine;

public abstract class UIBaseState
{
    public UIStateMachine.State state;

    public abstract void Enter(UIContext context);

    public abstract void Tick(UIContext context, UIStateMachine fsm, float deltaTime);

    public abstract void Exit(UIContext context);
}
