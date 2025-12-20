using System;
using UnityEngine;

public class UIStateMachine : MonoBehaviour
{
    public enum State
    {
        Idle,
        Attack,
        Build
    }
    
    private UIContext context;
    private UIBaseState currentState;

    public void SetContext(Database db)
    {
        context.SetMouseData(db.mouseDB);
    }

    private void SetCursor()
    {
        Debug.Log("Set cursor");
        switch(currentState.state)
        {
            case State.Idle:
                Cursor.SetCursor(context.GetMouseData().idle_cursor, Vector2.zero, CursorMode.Auto);
                break;
            case State.Attack:
                Cursor.SetCursor(context.GetMouseData().attack_cursor, Vector2.zero, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(context.GetMouseData().idle_cursor, Vector2.zero, CursorMode.Auto);
                break;
        }
    }

    void Awake()
    {
        context = GetComponent<UIContext>();
    }

    public void Tick(float deltaTime)
    {
        if (currentState == null) {ChangeState(new UIIdleState());}

        currentState.Tick(context, this, deltaTime);
    }

    public void ChangeState(UIBaseState next)
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
            SetCursor();
            currentState.Enter(context);
        }
    }
}
