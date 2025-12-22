using System;
using System.Collections.Generic;
using UnityEditor.Animations;
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
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("attack"))
        {
            return info.normalizedTime;
        }
        return -1;
    }

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
}
