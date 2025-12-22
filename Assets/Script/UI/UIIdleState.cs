using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIIdleState : UIBaseState
{
    private bool attack = false;

    private void CheckAttack() {attack = true;}

    private void CreateWorldClickEffect()
    {
       // Destroy를 행동이 끝났을 때... 
    }

    public override void Enter(UIContext context)
    {
        state = UIStateMachine.State.Idle;

        context.GetSelectArea().eSelect += context.GetInterface().SelectEvent;
        context.GetCommandPanel().eAttackCommand += CheckAttack;
    }

    public override void Tick(UIContext context, UIStateMachine fsm, float deltaTime)
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            // UI를 클릭한 것이 아닌 World를 클릭한 것을 체크 후.. 
            Vector3 target = context.MouseToGroundPosition();
            context.GetInterface().WorldRightClickEvent(target);
            GameObject go = Object.Instantiate(
                context.GetMouseData().right_click_effect, target + (Vector3.up * 0.1f), context.GetMouseData().right_click_effect.transform.rotation);
            Object.Destroy(go, 3.0f); // 나중에 행동이 끝났을 때 없어지는 것으로 변경..
        }
        context.GetSelectArea().Tick(deltaTime);

        if (attack || Keyboard.current.aKey.isPressed) // 유닛이 선택되어있는 상태..
        {
            fsm.ChangeState(new UIAttackState());
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            GameObject go = context.MouseToObject();
            // Debug.Log(go.name);
            if (go !=null)
            {
                context.GetInterface().WorldLeftClickEvent(go);
            }
        }
    }

    public override void Exit(UIContext context)
    {
        context.GetSelectArea().eSelect -= context.GetInterface().SelectEvent;
        context.GetCommandPanel().eAttackCommand -= CheckAttack;
    }
}
