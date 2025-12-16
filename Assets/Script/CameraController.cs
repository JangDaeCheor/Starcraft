using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float edgeSize = 5.0f;
    [SerializeField]
    private float speed = 1.0f;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;
        Vector2 mousePos = Mouse.current.position.ReadValue();
        // Debug.Log(mousePos);

        if (mousePos.x <= edgeSize)
        {
            move.x -= 1f;
        }
        if (mousePos.x >= Screen.width - edgeSize)
        {
            move.x += 1f;
        }
        if (mousePos.y <= edgeSize)
        {
            move.z -= 1f;
        }
        if (mousePos.y >= Screen.height - edgeSize)
        {
            move.z += 1f;
        }
        // Debug.Log(move);

        transform.Translate(move.normalized * speed, Space.World);
    }
}
