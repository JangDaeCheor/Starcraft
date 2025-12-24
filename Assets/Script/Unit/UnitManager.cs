using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField]
    private List<UnitStateMachine> units;
    [SerializeField]
    private List<UnitStateMachine> selectedUnits;

    // public event Action eGetData;
    public event Action<mGetData> eGetData;
    public event Action<UnitContext[]> eSelectedUnits;

    public void SpawnUnit(GameObject go)
    {
        UnitStateMachine unit = go.GetComponent<UnitStateMachine>();
        if (unit != null)
        {
            MessageBus.Publish<mGetData>(new mGetData(unit.SetData));
            units.Add(unit);
        }
    }

    public void ChangeData(mChangeData db)
    {
        foreach (UnitStateMachine unit in units)
        {
            unit.SetData(db.setData);
        }
    }

    // 나중에 화면에 있는 유닛만 선택되게 제한할 수 있나?
    public void CheckSelect(Vector2 startPos, Vector2 endPos)
    {
        Vector2 min = Vector2.Min(startPos, endPos);
        Vector2 max = Vector2.Max(startPos, endPos);
        Rect selectionRect = Rect.MinMaxRect(min.x, min.y, max.x, max.y);

        selectedUnits.Clear();

        foreach (UnitStateMachine unit in units)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(unit.transform.position);

            if (screenPos.z < 0) Debug.LogError("camera back!!");

            if (selectionRect.Contains(screenPos))
            {
                selectedUnits.Add(unit);
                unit.SetSeleted(true);
            } else
            {
                unit.SetSeleted(false);
            }
        }

        if (selectedUnits.Count > 0) {PublishSelectedUnits();}
    }

    public void MoveCommand(Vector3 target)
    {
        foreach (UnitStateMachine unit in selectedUnits)
        {
            unit.MoveCommand(target);
        }
    }
    public void AttackCommand(Vector3 target)
    {
        foreach (UnitStateMachine unit in selectedUnits)
        {
            unit.AttackCommand(target);
        }
    }

    private void PublishSelectedUnits()
    {
        List<UnitContext> unitArray = new List<UnitContext>();
        foreach (UnitStateMachine unit in selectedUnits)
        {
            unitArray.Add(unit.GetContext());
        }

        eSelectedUnits?.Invoke(unitArray.ToArray());
    }

    void Awake()
    {
        units.AddRange(FindObjectsByType<UnitStateMachine>(FindObjectsSortMode.None));
        foreach (UnitStateMachine unit in units)
        {
            MessageBus.Subscribe<mSetData>(unit.SetData);
        }

        MessageBus.Subscribe<mGetData>(eGetData);
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
