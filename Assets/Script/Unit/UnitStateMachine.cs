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

    private UnitContext context;
    private IUnitBaseState currentState;

    public void MoveCommand(Vector3 target)
    {
        if (context.GetSeleted())
        {
            context.moveTarget = target;
            ChangeState(new UnitMoveState());
        }
    }

    public void SetData(Database db)
    {
        unit unitData = db.SelectUnitName(unitName);
        skill skillData = db.SelectSkillId(unitData.skill_id);
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
            currentState.FixedTick(context, deltaTime);
        }
    }

    public void ChangeState(IUnitBaseState next)
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
        }
    }
}
