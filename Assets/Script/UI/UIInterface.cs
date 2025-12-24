using System;
using UnityEngine;

public class UIInterface : MonoBehaviour
{
    public UIStateMachine fsm;

    public event Action<mGetData> eGetData;
    
    public void SetData(mSetData setData) {fsm.SetContext(setData.db);}
    public void ChangeData(mChangeData changeData) {fsm.SetContext(changeData.setData.db);}

    public event Action<Vector3> eWorldRightClick;
    public event Action<GameObject> eWorldLeftClick;
    public event Action<Vector2, Vector2> eSelect;
    public event Action<Vector3> eAttack;
    public event Action<GameObject> eSpawnUnit; 

    public void WorldRightClickEvent(Vector3 target) {eWorldRightClick?.Invoke(target);}
    public void WorldLeftClickEvent(GameObject target) {eWorldLeftClick?.Invoke(target);}
    public void SelectEvent(Vector2 start, Vector2 end) {eSelect?.Invoke(start, end);}
    public void SelectedUnitEvent(UnitContext[] selectedUnits) {fsm.SetSelectedUnits(selectedUnits);}
    public void SelectedBuildingEvent(unit[] units, skill[] skills) {fsm.SetSelectedBuilding(units, skills);}
    public void AttackEvent(Vector3 target) {eAttack?.Invoke(target);}
    public void SpawnUnit(GameObject prefab) {eSpawnUnit?.Invoke(prefab);}


    void Awake()
    {
        fsm = GetComponent<UIStateMachine>();

        MessageBus.Subscribe<mGetData>(eGetData);
        MessageBus.Subscribe<mSetData>(SetData);
        MessageBus.Subscribe<mChangeData>(ChangeData);
    }
}
