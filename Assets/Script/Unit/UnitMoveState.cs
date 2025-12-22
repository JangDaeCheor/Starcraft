using UnityEngine;
using UnityEngine.AI;

public class UnitMoveState : UnitBaseState
{
    private NavMeshAgent agent;

    public override void Enter(UnitContext context)
    {
        state = UnitStateMachine.State.Move;

        agent = context.GetNavMeshAgent();

        context.GetAnimationBridge().SetMove(true);
        agent.isStopped = false;
    }

    public override void Tick(UnitContext context, UnitStateMachine fsm, float deltaTime)
    {
        if (agent == null)
        {
            Debug.LogError("not agent!!");
            fsm.ChangeState(new UnitIdleState());
            return;
        }

        if (agent.isStopped)
        {
            fsm.ChangeState(new UnitIdleState());
        }
    }

    public override void FixedTick(UnitContext context, UnitStateMachine fsm, float deltaTime)
    {
        if (agent == null)
        {
            return;
        }

        // 꼭 여기서 거리를 판단해야 하나? Update쪽이 낫지 않을까?
        float dist = Vector3.Distance(context.GetTransform().position, context.moveTarget);
        if (dist <= context.reachThreshold)
        {
            agent.isStopped = true;
        } else
        {
            Debug.DrawRay(context.GetTransform().position, context.moveTarget, Color.red);
            agent.SetDestination(context.moveTarget);
        }        
    }

    public override void Exit(UnitContext context)
    {
        context.GetAnimationBridge().SetMove(false);
    }
}
