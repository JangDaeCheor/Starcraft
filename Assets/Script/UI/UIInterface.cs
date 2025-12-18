using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInterface : MonoBehaviour
{
    public Database db;

    [SerializeField]
    private MouseSelectArea selectArea;
    [SerializeField]
    private mouse mouseData;

    public event Action<Vector3> mapClick;
    public event Action<Vector2, Vector2> select;

    public void Tick(float deltaTime)
    {
        selectArea.Tick(deltaTime);

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Vector2 mouse = Mouse.current.position.value;
            Ray ray = Camera.main.ScreenPointToRay(
                new Vector3(mouse.x, mouse.y, 0));
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

            bool blocked = Physics.Raycast(ray, out hit, 100, mouseData.ground);

            if (blocked)
            {
                mapClick?.Invoke(hit.point);

                GameObject go = Instantiate(
                    mouseData.right_click_effect, hit.point + (Vector3.up * 0.1f), mouseData.right_click_effect.transform.rotation);
                Destroy(go, mouseData.left_mark_time);
            }
        }
    }

    private void ConnectSelect(Vector2 start, Vector2 end)
    {
        select?.Invoke(start, end);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mouseData = db.mouseDB;
        
        selectArea = GetComponentInChildren<MouseSelectArea>();
        selectArea.select += ConnectSelect;
    }
}
