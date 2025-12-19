using System;
using UnityEngine;
using UnityEngine.UI;

public class CommandPanel : MonoBehaviour
{
    [SerializeField]
    private Button btnAttack;

    public event Action eAttack;

    public void OnTestClick()
    {
        Debug.Log("click");
    }

    void Awake()
    {
        Button[] buttons = GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            if (button.name == "Attack")
            {
                btnAttack = button;
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        btnAttack.onClick.AddListener(()=>eAttack?.Invoke());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
