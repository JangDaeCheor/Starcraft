using UnityEngine;
using UnityEngine.AI;

public class UnitMoveState : IUnitBaseState
{
    public UnitStateMachine.State state = UnitStateMachine.State.Move;

    public virtual void Enter(UnitContext context)
    {
        context.GetAnimationBridge().SetMove(true);
    }

    public virtual void Tick(UnitContext context, UnitStateMachine fsm, float deltaTime)
    {
        NavMeshAgent agent = context.GetNavMeshAgent();
        if (agent == null)
        {
            Debug.LogError("not agent!!");
            fsm.ChangeState(new UnitIdleState());
            return;
        }
    }

    public virtual void FixedTick(UnitContext context, float deltaTime)
    {
        NavMeshAgent agent = context.GetNavMeshAgent();
        if (agent == null)
        {
            return;
        }

        float dist = Vector3.Distance(context.GetTransform().position, context.moveTarget);
        if (dist <= context.reachThreshold)
        {
            agent.isStopped = true;
        } else
        {
            Debug.DrawRay(context.GetTransform().position, context.moveTarget, Color.red);
            agent.SetDestination(context.moveTarget);
            agent.isStopped = false;
        }        
    }

    public virtual void Exit(UnitContext context)
    {
        context.GetAnimationBridge().SetMove(false);
    }
}
