using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField]
    private List<UnitStateMachine> units;

    public event Action eGetData;
    public event Action<Database> eSetData;

    public void SetData(Database db)
    {
        eSetData?.Invoke(db);
        eSetData = null;
    }

    public void ChangeData(Database db)
    {
        foreach (UnitStateMachine unit in units)
        {
            eSetData += unit.SetData;
        }
        eSetData?.Invoke(db);
        eSetData = null;
    }

    // 나중에 화면에 있는 유닛만 선택되게 제한할 수 있나?
    public void CheckSelect(Vector2 startPos, Vector2 endPos)
    {
        Vector2 min = Vector2.Min(startPos, endPos);
        Vector2 max = Vector2.Max(startPos, endPos);
        Rect selectionRect = Rect.MinMaxRect(min.x, min.y, max.x, max.y);

        foreach (UnitStateMachine unit in units)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(unit.transform.position);

            if (screenPos.z < 0) Debug.LogError("camera back!!");

            if (selectionRect.Contains(screenPos))
            {
                unit.SetSeleted(true);
            } else
            {
                unit.SetSeleted(false);
            }
        }
    }

    // 나중에 화면에 있는 유닛만 선택되게 제한할 수 있나?
    public void MoveCommand(Vector3 target)
    {
        foreach (UnitStateMachine unit in units)
        {
            unit.MoveCommand(target);
        }
    }

    void Awake()
    {
        units.AddRange(FindObjectsByType<UnitStateMachine>(FindObjectsSortMode.None));
        foreach (UnitStateMachine unit in units)
        {
            eSetData += unit.SetData;
        }
    }

    public void Tick(float deltaTime)
    {
        foreach (UnitStateMachine unit in units)
        {
            unit.Tick(deltaTime);
        }
    }

    public void FixedTick(float deltaTime)
    {
        foreach (UnitStateMachine unit in units)
        {
            unit.FixedTick(deltaTime);
        }
    }
}
