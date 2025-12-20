using UnityEngine;

public class UnitIdleState : IUnitBaseState
{
    public UnitStateMachine.State state = UnitStateMachine.State.Idle;

    public virtual void Enter(UnitContext context)
    {
        
    }

    public virtual void Tick(UnitContext context, UnitStateMachine fsm, float deltaTime)
    {
        
    }

    public virtual void FixedTick(UnitContext context, float deltaTime)
    {
        
    }

    public virtual void Exit(UnitContext context)
    {
        
    }
}
