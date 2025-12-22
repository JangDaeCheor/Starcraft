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

        _commandPanel.eSpawnCommand += _interface.SpawnUnit;
    }

    public void SetMouseData(mouse mouse) {_mouseData = mouse;}
    public void SetCommandData(ui_command data) {_commandPanel.SetData(data);}

    public mouse GetMouseData() {return _mouseData;}
    public UIInterface GetInterface() {return _interface;}
    public CommandPanel GetCommandPanel() {return _commandPanel;}
    public MouseSelectArea GetSelectArea() {return _selectArea;}

    public Vector3 MouseToGroundPosition()
    {
        Vector2 mouse = Mouse.current.position.value;
        Ray ray = Camera.main.ScreenPointToRay(
            new Vector3(mouse.x, mouse.y, 0));
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

        bool blocked = Physics.Raycast(ray, out hit, 100, _mouseData.ground_layer);

        if (blocked)
        {
            return hit.point;
        } else
        {
            return transform.position; // canvas는 안 움직이겠지?...
        }
    }

    public GameObject MouseToObject()
    {
        Vector2 mouse = Mouse.current.position.value;
        Ray ray = Camera.main.ScreenPointToRay(
            new Vector3(mouse.x, mouse.y, 0));
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

        bool blocked = Physics.Raycast(ray, out hit, 100, _mouseData.object_layer);

        if (blocked)
        {
            return hit.collider.gameObject;
        } else
        {
            return null;
        }
    }
}
