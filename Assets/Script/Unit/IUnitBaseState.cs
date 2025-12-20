using UnityEngine;

public interface IUnitBaseState
{
    void Enter(UnitContext context);

    void Tick(UnitContext context, UnitStateMachine fsm, float deltaTime);

    void FixedTick(UnitContext context, float deltaTime);

    void Exit(UnitContext context);
}
