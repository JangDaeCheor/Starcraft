using UnityEngine;
using UnityEngine.InputSystem;

public class UIIdleState : UIBaseState
{
    private bool attack = false;

    private void CheckAttack() {attack = true;}

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
            context.GetInterface().WorldClickEvent(context.MouseToWorldPosition());
        }
        context.GetSelectArea().Tick(deltaTime);

        if (attack || Keyboard.current.aKey.isPressed)
        {
            fsm.ChangeState(new UIAttackState());
        }
    }

    public override void Exit(UIContext context)
    {
        context.GetSelectArea().eSelect -= context.GetInterface().SelectEvent;
        context.GetCommandPanel().eAttackCommand -= CheckAttack;
    }
}
