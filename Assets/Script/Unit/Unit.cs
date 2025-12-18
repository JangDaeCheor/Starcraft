using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
    public enum State
    {
        Idle,
        Run,
        Attack,
        Die
    }
    // public Action<string> GetUnitData;
    // public Action<int> GetSkillData;
    public Database db;
    public State state;

    [SerializeField]
    private string unitName;
    [SerializeField]
    private unit unit;
    [SerializeField]
    private skill skill;

    [SerializeField]
    private UnitMove unitMove;
    [SerializeField]
    private AnimationBridge aniBridge;

    [SerializeField]
    private GameObject selectedMark;
    [SerializeField]
    private bool isSelected = false;

    void Awake()
    {
        unitMove = GetComponent<UnitMove>();
        aniBridge = GetComponentInChildren<AnimationBridge>();

        selectedMark.SetActive(false);

        state = State.Idle; // 주의
    }

    void Start()
    {
        if (db != null)
        {
            unit = db.SelectUnitName(unitName);
            skill = db.SelectSkillId(unit.skill_id);
        } else
        {
            Debug.LogError("not database!!");
        }

        if (unitMove != null)
        {
            unitMove.speed = unit.move_speed;
        } else
        {
            Debug.LogWarning("not unit move!!");
        }
    }

    void ChangeState(State newState)
    {
        state = newState;
        aniBridge.SetAnimation();
    }

    public void MoveTo(Vector3 target)
    {
        if (isSelected == false)
        {
            return;
        }

        unitMove.SetTarget(target);

        if (state == State.Idle) // 주의 추가될 경우 있음.
        {
            ChangeState(State.Run);
        }
    }

    public void FixedTick(float deltaTime)
    {
        if (isSelected)
        {
            unitMove.FixedTick(deltaTime);
        }

        if (state == State.Run)
        {
            if (unitMove.move == false)
            {
                ChangeState(State.Idle);
            }
        }
    }

    public void Tick(float deltaTime)
    {
        aniBridge.Tick(deltaTime);
    }

    public void CheckSelect(Vector2 startPos, Vector2 endPos)
    {
        Vector2 min = Vector2.Min(startPos, endPos);
        Vector2 max = Vector2.Max(startPos, endPos);
        Rect selectionRect = Rect.MinMaxRect(min.x, min.y, max.x, max.y);

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        
        if (screenPos.z < 0) Debug.LogError("camera back!!");

        if (selectionRect.Contains(screenPos))
        {
            isSelected = true;
            // Debug.Log("select!!");
            selectedMark.SetActive(true);
        } else
        {
            isSelected = false;
            selectedMark.SetActive(false);
        }
    }
}
