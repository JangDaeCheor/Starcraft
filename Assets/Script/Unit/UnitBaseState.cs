using UnityEngine;

public abstract class UnitBaseState
{
    public UnitStateMachine.State state;

    public abstract void Enter(UnitContext context);

    public abstract void Tick(UnitContext context, UnitStateMachine fsm, float deltaTime);

    public abstract void FixedTick(UnitContext context, UnitStateMachine fsm, float deltaTime);

    public abstract void Exit(UnitContext context);
}
