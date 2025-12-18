using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBridge : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Unit unit;

    [SerializeField]
    public event Action attackMotionFinish;
    private bool attack;

    /// <summary>
    /// IsMove : Bool, Attack : Trigger, AttackMotionTime : Float
    /// </summary>
    public void SetAnimation()
    {
        if (unit.state == Unit.State.Idle)
        {
            animator.SetBool("IsMove", false); // 파라미터 이름이 달라도 error 없음.
            animator.ResetTrigger("Attack");
        } else if (unit.state == Unit.State.Run)
        {
            animator.SetBool("IsMove", true);
        } else if (unit.state == Unit.State.Attack)
        {
            attack = true;
            animator.SetTrigger("Attack");
        }
    }

    public void Tick(float deltaTime)
    {
        // animator.Get은 parameter가 없을 경우 warning
        if (animator.GetFloat("AttackMotionTime") > 0.9)
        {
            if (attack)
            {
                attackMotionFinish?.Invoke();
                attack = false;
            }
        }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        unit = GetComponentInParent<Unit>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetAnimation();
    }
}
