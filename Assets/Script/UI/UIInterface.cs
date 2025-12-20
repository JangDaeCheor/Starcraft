using System;
using UnityEngine;

public class UIInterface : MonoBehaviour
{
    public UIStateMachine fsm;

    public event Action eGetData;
    
    public event Action<Vector3> eWorldClick;
    public event Action<Vector2, Vector2> eSelect;
    public event Action<Vector3> eAttack;

    public void GetDataEvent() {eGetData?.Invoke();}

    public void WorldClickEvent(Vector3 target) {eWorldClick?.Invoke(target);}
    public void SelectEvent(Vector2 start, Vector2 end) {eSelect?.Invoke(start, end);}
    public void AttackEvent(Vector3 target) {eAttack?.Invoke(target);}

    void Awake()
    {
        fsm = GetComponent<UIStateMachine>();
    }

    public void SetData(Database db) {fsm.SetContext(db);}
}
