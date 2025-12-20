using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBridge : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    public void SetMove(bool move)
    {
        animator.SetBool("IsMove", move);
    }

    public void SetAttackTrigger()
    {
        animator.SetTrigger("Attack");
    }

    public void ResetAttackTrigger()
    {
        animator.ResetTrigger("Attack");
    }

    public float GetAttackMotionTime()
    {
        return animator.GetFloat("AttackMotionTime");
    }

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
}
