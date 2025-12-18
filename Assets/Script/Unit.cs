using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
    // public Action<string> GetUnitData;
    // public Action<int> GetSkillData;
    public Database db;

    [SerializeField]
    private string unitName;
    [SerializeField]
    private unit unit;
    [SerializeField]
    private skill skill;

    // 임시
    [SerializeField]
    private mouse mouse;

    [SerializeField]
    private UnitMove unitMove;

    [SerializeField]
    private GameObject selectedMark;
    [SerializeField]
    private bool isSelected = false;

    void Awake()
    {
        unitMove = GetComponent<UnitMove>();
        selectedMark.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (db != null)
        {
            unit = db.SelectUnitName(unitName);
            skill = db.SelectSkillId(unit.skill_id);
            mouse = db.mouseDB;
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

    public void FixedTick(float deltaTime)
    {
        if (isSelected)
        {
            unitMove.FixedTick(deltaTime);
        }
    }

    // utill로 옮겨야 할까?
    public void CheckSelect(Vector2 startPos, Vector2 endPos)
    {
        // Ray ray = Camera.main.ScreenPointToRay(
        //     new Vector3(endPos.x, endPos.y, 0));
        // RaycastHit hit;

        // Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

        // bool blocked = Physics.Raycast(ray, out hit, 100, mouse.ground);

        // if (blocked)
        // {
        //     GameObject go = Instantiate(
        //         mouse.right_click_effect, hit.point + (Vector3.up * 0.1f), mouse.right_click_effect.transform.rotation);
        // }

        Vector2 min = Vector2.Min(startPos, endPos);
        Vector2 max = Vector2.Max(startPos, endPos);
        Rect selectionRect = Rect.MinMaxRect(min.x, min.y, max.x, max.y);

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        
        // GameObject go = Instantiate(
        //     mouse.right_click_effect, screenPos, Quaternion.identity);

        if (screenPos.z < 0) Debug.LogError("camera back!!");

        if (selectionRect.Contains(screenPos))
        {
            Debug.Log("select!!");
            selectedMark.SetActive(true);
        } else
        {
            selectedMark.SetActive(false);
        }
    }
}
