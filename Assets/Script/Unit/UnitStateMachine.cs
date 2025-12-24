using System;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEngine;

public class UnitStateMachine : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move,
        Attack
    }

    [SerializeField]
    public string unitName;
    [SerializeField]
    private GameObject _seletedMark; // 여기서 지정.
    [SerializeField]
    private State state;

    private UnitContext context;
    private UnitBaseState currentState;

    public event Action<float> attack;

    public UnitContext GetContext() {return context;}

    public void MoveCommand(Vector3 target)
    {
        if (context.GetSeleted())
        {
            context.moveTarget = target;
            ChangeState(new UnitMoveState());
        }
    }
    public void AttackCommand(Vector3 target)
    {
        if (context.GetSeleted())
        {
            context.moveTarget = target;
            ChangeState(new UnitAttackState());
        }
    }

    public void Attack(float damage)
    {
        attack?.Invoke(damage);
    }
    public void HitDamage(float damage)
    {
        context.GetHealth().HitDamage(damage);
    }

    public void SetData(mSetData setData)
    {
        // Debug.Log("SetData : " + this.name);
        unit unitData = setData.db.SelectUnitName(unitName);
        skill skillData = setData.db.SelectSkillId(unitData.skill_id);
        context.SetContext(unitData, skillData, _seletedMark);
    }

    public void SetSeleted(bool seleted)
    {
        context.SetSeleted(seleted);
    }

    void Awake()
    {
        context = GetComponent<UnitContext>();
    }

    public void Tick(float deltaTime)
    {
        if (currentState != null)
        {
            currentState.Tick(context, this, deltaTime);
        }
    }

    public void FixedTick(float deltaTime)
    {
        if (currentState != null)
        {
            currentState.FixedTick(context, this, deltaTime);
        }
    }

    public void ChangeState(UnitBaseState next)
    {
        if (context == null)
        {
            Debug.LogError("not unit context");
            return;
        }

        if (currentState != null)
        {
            currentState.Exit(context);
        }
        currentState = next;
        if (currentState != null)
        {
            currentState.Enter(context);
            state = currentState.state;
        }
    }
}
