using UnityEngine;

public class UnitIdleState : UnitBaseState
{
    public override void Enter(UnitContext context)
    {
        state = UnitStateMachine.State.Idle;
    }

    public override void Tick(UnitContext context, UnitStateMachine fsm, float deltaTime)
    {
        
    }

    public override void FixedTick(UnitContext context, UnitStateMachine fsm, float deltaTime)
    {
        
    }

    public override void Exit(UnitContext context)
    {
        
    }
}
