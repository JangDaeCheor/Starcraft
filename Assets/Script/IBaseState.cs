using UnityEngine;

public interface IBaseState
{
    void Enter(UnitContext context);

    void Tick(UnitContext context, UnitStateMachine fsm, float deltaTime);

    void FixedTick(UnitContext context, float deltaTime);

    void Exit(UnitContext context);
}
