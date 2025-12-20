using System;
using UnityEngine;
using UnityEngine.UI;

public class CommandPanel : MonoBehaviour
{
    [SerializeField]
    private Button btnAttack;

    public event Action eAttackCommand;

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

        btnAttack.onClick.AddListener(()=>eAttackCommand?.Invoke());
    }
}
