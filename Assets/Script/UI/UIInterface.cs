using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInterface : MonoBehaviour
{
    public Database db;
    [SerializeField]
    private mouse mouseData;

    [SerializeField]
    private MouseSelectArea selectArea;
    [SerializeField]
    private CommandPanel commandPanel;

    public event Action<Vector3> eMapClick;
    public event Action<Vector2, Vector2> eSelect;
    public event Action eAttack;

    void MapClick()
    {
        Vector2 mouse = Mouse.current.position.value;
        Ray ray = Camera.main.ScreenPointToRay(
            new Vector3(mouse.x, mouse.y, 0));
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

        bool blocked = Physics.Raycast(ray, out hit, 100, mouseData.ground);

        if (blocked)
        {
            eMapClick?.Invoke(hit.point);

            GameObject go = Instantiate(
                mouseData.right_click_effect, hit.point + (Vector3.up * 0.1f), mouseData.right_click_effect.transform.rotation);
            Destroy(go, mouseData.left_mark_time);
        }
    }

    void AttackCursor()
    {
        
    }

    public void Tick(float deltaTime)
    {
        selectArea.Tick(deltaTime);

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            MapClick();
        }
        // ShortCutKey를 나중에 만들어야함.
        if (Mouse.current.leftButton.isPressed && Keyboard.current.aKey.isPressed)
        {
            AttackCursor();
        }
    }

    void Awake()
    {
        selectArea = GetComponentInChildren<MouseSelectArea>();
        commandPanel = GetComponentInChildren<CommandPanel>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mouseData = db.mouseDB;
        
        selectArea.eSelect += (Vector2 start, Vector2 end)=>eSelect?.Invoke(start, end);
        commandPanel.eAttack += ()=>eAttack.Invoke();
    }
}
