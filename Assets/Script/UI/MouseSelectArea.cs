using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MouseSelectArea : MonoBehaviour
{
    private RectTransform selectArea;
    private Image image;
    private Vector2 startPos;
    private Vector2 endPos;

    public event Action<Vector2, Vector2> eSelect;

    void Awake()
    {
        selectArea = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        image.enabled = false;
        // selectArea.gameObject.SetActive(false);
    }

    public void Tick(float deltaTime)
    {
        // Debug.Log(Mouse.current.position.value);
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Debug.Log("mouse down");
            startPos = Mouse.current.position.value;
            endPos = Mouse.current.position.value;
        }

        if (Mouse.current.leftButton.isPressed)
        {
            endPos = Mouse.current.position.value;

            Vector2 min = Vector2.Min(startPos, endPos);
            Vector2 max = Vector2.Max(startPos, endPos);

            // Canvas Scaler를 껐기 때문에 가능.
            // Canvas Scaler를 쓸 경우 RectTransformUtility.ScreenPointToLocalPointInRectangle 필요.
            selectArea.anchoredPosition = min;
            selectArea.sizeDelta = max - min;

            image.enabled = true;
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            // Debug.Log("mouse up");
            image.enabled = false;
            eSelect?.Invoke(startPos, endPos);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Tick(Time.deltaTime);
    }
}
