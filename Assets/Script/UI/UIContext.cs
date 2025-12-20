using UnityEngine;
using UnityEngine.InputSystem;

public class UIContext : MonoBehaviour
{
    private mouse _mouseData;

    private CommandPanel _commandPanel;
    private MouseSelectArea _selectArea;
    private UIInterface _interface;

    void Awake()
    {
        _interface = GetComponent<UIInterface>();
        _commandPanel = GetComponentInChildren<CommandPanel>();
        _selectArea = GetComponentInChildren<MouseSelectArea>();
    }

    public void SetMouseData(mouse mouse) {_mouseData = mouse;}

    public mouse GetMouseData() {return _mouseData;}
    public UIInterface GetInterface() {return _interface;}
    public CommandPanel GetCommandPanel() {return _commandPanel;}
    public MouseSelectArea GetSelectArea() {return _selectArea;}

    public Vector3 MouseToWorldPosition()
    {
        Vector2 mouse = Mouse.current.position.value;
        Ray ray = Camera.main.ScreenPointToRay(
            new Vector3(mouse.x, mouse.y, 0));
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

        bool blocked = Physics.Raycast(ray, out hit, 100, _mouseData.ground);

        if (blocked)
        {
            return hit.point;
        } else
        {
            return transform.position; // canvas는 안 움직이겠지?...
        }
    }
}
