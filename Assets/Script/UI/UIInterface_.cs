using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInterface_ : MonoBehaviour
{
    public enum State
    {
        Idle,
        Attack,
        Build
    }

    public State state;

    private mouse mouseData;

    private MouseSelectArea selectArea;
    private CommandPanel commandPanel;

    public event Action eGetData;

    public event Action<Vector3> eWroldClick;
    public event Action<Vector2, Vector2> eSelect;
    public event Action eAttack;

    public void SetData(Database db)
    {
        mouseData = db.mouseDB;
    }

    public void InvokeWorldClick()
    {
        Vector3 world = MouseToWorldPosition();

        if (world != transform.position)
        {
            eWroldClick?.Invoke(MouseToWorldPosition());
        }
    }

    private Vector3 MouseToWorldPosition()
    {
        Vector2 mouse = Mouse.current.position.value;
        Ray ray = Camera.main.ScreenPointToRay(
            new Vector3(mouse.x, mouse.y, 0));
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

        bool blocked = Physics.Raycast(ray, out hit, 100, mouseData.ground);

        if (blocked)
        {
            return hit.point;
        } else
        {
            return transform.position; // canvas는 안 움직이겠지?...
        }
    }

    void MapClick()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            // 
        }
    }

    void AttackCursor()
    {
        
    }

    void Awake()
    {
        selectArea = GetComponentInChildren<MouseSelectArea>();
        commandPanel = GetComponentInChildren<CommandPanel>();

        selectArea.eSelect += (Vector2 start, Vector2 end)=>eSelect?.Invoke(start, end);
        // commandPanel.eAttack += ()=>eAttack.Invoke();
    }

    public void Tick(float deltaTime)
    {
        selectArea.Tick(deltaTime);

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            state = State.Idle;
            MapClick();
        }
        // ShortCutKey를 나중에 만들어야함.
        if (Keyboard.current.aKey.isPressed)
        {
            if (state == State.Idle)
            {
                state = State.Attack;
                AttackCursor();
            }
        }
    }
}
