using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAttackState : UnitBaseState
{
    private NavMeshAgent agent;
    private GameObject _attackTarget = null;
    private float coolTime = 0.0f;
    private bool attack = false;

    private void CheckAttackTarget(UnitContext context, UnitStateMachine fsm)
    {
        if (_attackTarget != null)
        {
            return;
        }

        List<GameObject> targets = context.GetDetectedEnemy();
        if (targets.Count != 0)
        {
            GameObject attackTarget = null;
            foreach (GameObject target in targets)
            {
                Vector3 toTarget = target.transform.position - context.GetTransform().position;
                Vector3 toAttackTarget = Vector3.zero;
                if (attackTarget != null) {toAttackTarget = attackTarget.transform.position - context.GetTransform().position;}

                if (toAttackTarget.magnitude < toTarget.magnitude)
                {
                    attackTarget = target;
                }
            }

            _attackTarget = attackTarget;

            UnitStateMachine enemyFSM = _attackTarget.GetComponent<UnitStateMachine>();
            fsm.attack += enemyFSM.HitDamage;
            return;
        }
    }

    private void AttackTarget(
        UnitContext context, UnitStateMachine fsm, AnimationBridge bridge, float deltaTime, 
        Vector3 minePoint, Vector3 enemyPoint)
    {
        coolTime -= deltaTime;

        if (coolTime <= 0)
        {
            coolTime = context.GetSkillData().cooltime;

            bridge.SetAttackTrigger();
            attack = false;
        }

        if (bridge.GetAttackMotionTime() > 0.5f && attack == false)
        {
            fsm.Attack(context.GetSkillData().damage);
            attack = true;

            if (context.GetSkillData().name == "Gun")
            {
                GameObject go = GameObject.Instantiate(
                    context.GetSkillData().effect, enemyPoint, context.GetSkillData().effect.transform.rotation);
            }
            if (context.GetSkillData().name == "Fire")
            {
                GameObject go = GameObject.Instantiate(
                    context.GetSkillData().effect, minePoint, context.GetTransform().rotation.normalized);
            }
        }
    }

    public override void Enter(UnitContext context)
    {
        state = UnitStateMachine.State.Attack;

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

        CheckAttackTarget(context, fsm);
        if (_attackTarget != null) 
        {
            context.moveTarget = _attackTarget.transform.position;
        }

        if (_attackTarget == null && agent.isStopped)
        {
            fsm.ChangeState(new UnitIdleState());
        }
    }

    public override void FixedTick(UnitContext context, UnitStateMachine fsm, float deltaTime)
    {
        agent.SetDestination(context.moveTarget);

        if (_attackTarget != null)
        {
            Collider mine = context.GetCollider();
            Collider enemy = _attackTarget.GetComponent<Collider>();

            Vector3 point1 = mine.ClosestPoint(enemy.transform.position);
            Vector3 point2 = enemy.ClosestPoint(mine.transform.position);

            if (Vector3.Distance(point1, point2) < context.GetSkillData().attack_distance)
            {
                AnimationBridge bridge = context.GetAnimationBridge();

                agent.isStopped = true;
                bridge.SetMove(false);

                AttackTarget(context, fsm, bridge, deltaTime, point1, point2);
            }
        } else
        {
            float dist = Vector3.Distance(context.GetTransform().position, context.moveTarget);
            if (dist <= context.reachThreshold)
            {
                agent.isStopped = true;
            }
        }
    }

    public override void Exit(UnitContext context)
    {
        context.GetAnimationBridge().ResetAttackTrigger();
        context.GetAnimationBridge().SetMove(false);
    }
}
