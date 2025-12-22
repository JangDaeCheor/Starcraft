using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandPanel : MonoBehaviour
{
    public enum State
    {
        Unit,
        UpgradeBuilding,
        SpawnBuilding
    }
    public State state = State.Unit;

    private Button[] buttons;
    private ui_command _data;

    public event Action eAttackCommand;
    public event Action<GameObject> eSpawnCommand;

    public void OnTestClick()
    {
        Debug.Log("click");
    }

    public void SetData(ui_command data) 
    {
        _data = data;
    }

    private void SetButtonImage(Button button, Texture2D image)
    {
        if (image == null)
        {
            button.image.sprite = Sprite.Create(
                _data.basic, 
                new Rect(0, 0, _data.basic.width, _data.basic.height), 
                new Vector2(0.5f, 0.5f));
        } else
        {
            button.image.sprite = Sprite.Create(
                image, 
                new Rect(0, 0, image.width, image.height), 
                new Vector2(0.5f, 0.5f));
        }
    }

    private void SetButtonsImage(Texture2D[] images)
    {
        ClearButtons();

        if (images.Length <= 9)
        {
            for (int i = 0;i < images.Length;i++)
            {
                SetButtonImage(buttons[i], images[i]);
            }

            if (state == State.Unit)
            {
                SetButtonImage(buttons[2], _data.attack);
            }
            
        } else if (images.Length > 9) {Debug.LogError("button image too much");}
    }

    private void ClearButtons()
    {
        foreach (Button button in buttons)
        {
            SetButtonImage(button, null);
        }
    }

    private void RemoveBtnConnect()
    {
        foreach (Button button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    public void SetUnitButtons(UnitContext[] units)
    {
        RemoveBtnConnect();

        state = State.Unit;

        SetButtonsImage(new Texture2D[1]{null});
        buttons[2].onClick.AddListener(()=>eAttackCommand?.Invoke());
    }

    public void SetBuildingButtons(unit[] units, skill[] skills)
    {
        RemoveBtnConnect();

        List<Texture2D> images = new List<Texture2D>();
        if (units.Length > 0)
        {
            state = State.SpawnBuilding;
            for (int i = 0;i < units.Length;i++) 
            {
                images.Add(units[i].unit_image);
                int index = i; // 람다 주의... i 사용 불가.
                buttons[i].onClick.AddListener(()=>eSpawnCommand?.Invoke(units[index].unit_prefab));
            }
        } else
        {
            state = State.UpgradeBuilding;
            for (int i = 0;i < skills.Length;i++) 
            {
                images.Add(skills[i].skill_image);
                // buttons[i].onClick.AddListener(()=>eSpawnCommand?.Invoke(units[i]));
            }
        }

        SetButtonsImage(images.ToArray());
    }

    void Awake()
    {
        buttons = GetComponentsInChildren<Button>();

        // btnAttack.onClick.AddListener(()=>eAttackCommand?.Invoke());
    }
}
