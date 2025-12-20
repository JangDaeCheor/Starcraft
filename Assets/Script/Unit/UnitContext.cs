using UnityEngine;
using UnityEngine.AI;

public class UnitContext : MonoBehaviour
{
    private unit _unitData;
    private skill _skillData;

    private AnimationBridge _aniBridge;
    private NavMeshAgent _agent;
    private Transform _transform;

    private GameObject _selectedMark;
    [SerializeField]
    private bool _isSelected = false;

    [Header("Move")]
    public Vector3 moveTarget;
    public float reachThreshold = 0.5f;
    public float turnSpeedDeg = 360.0f;

    void Awake()
    {
        _aniBridge = GetComponent<AnimationBridge>();
        _agent = GetComponent<NavMeshAgent>();
        _transform = transform;
    }

    public void SetContext(
        unit unitData, skill skillData, GameObject selectedMark)
    {
        _unitData = unitData;
        _skillData = skillData;
        _selectedMark = selectedMark;
    }

    public void SetSeleted(bool seleted)
    {
        _selectedMark.SetActive(seleted);
        _isSelected = seleted;
    }

    public unit GetUnitData() {return _unitData;}
    public skill GetSkillData() {return _skillData;}
    public AnimationBridge GetAnimationBridge() {return _aniBridge;}
    public NavMeshAgent GetNavMeshAgent() {return _agent;}
    public Transform GetTransform() {return _transform;}
    public bool GetSeleted() {return _selectedMark;}
}
