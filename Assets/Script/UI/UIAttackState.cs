using UnityEngine;
using UnityEngine.InputSystem;

public class UIAttackState : UIBaseState
{
    public override void Enter(UIContext context)
    {
        state = UIStateMachine.State.Attack;
    }

    public override void Tick(UIContext context, UIStateMachine fsm, float deltaTime)
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // UI를 클릭한 것이 아닌 World를 클릭한 것을 체크 후.. 
            Vector3 target = context.MouseToWorldPosition();
            context.GetInterface().AttackEvent(target);
            GameObject go = Object.Instantiate(
                context.GetMouseData().attack_effect, target + (Vector3.up * 0.1f), context.GetMouseData().attack_effect.transform.rotation);
            Object.Destroy(go, 3.0f); // 나중에 행동이 끝났을 때 없어지는 것으로 변경..

            fsm.ChangeState(new UIIdleState());
            return;
        }
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            fsm.ChangeState(new UIIdleState());
            return;
        }
    }

    public override void Exit(UIContext context)
    {
    }

}
