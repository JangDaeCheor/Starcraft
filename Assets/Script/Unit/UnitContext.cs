using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitContext : MonoBehaviour
{
    private unit _unitData;
    private skill _skillData;

    private AnimationBridge _aniBridge;
    private NavMeshAgent _agent;
    private Transform _transform;
    private Collider _collider;
    private Health _health;

    private GameObject _selectedMark;
    [SerializeField]
    private bool _isSelected = false;

    private LayerMask _enemyLayer;

    [Header("Move")]
    public Vector3 moveTarget;
    public float reachThreshold = 0.5f;
    public float turnSpeedDeg = 360.0f;

    void Awake()
    {
        _aniBridge = GetComponent<AnimationBridge>();
        _agent = GetComponent<NavMeshAgent>();
        _transform = transform;
        _collider = GetComponent<Collider>();
        _health = GetComponent<Health>();
        
        List<string> layerName = new List<string> (new string[] {"Player1", "Player2", "Player3"});
        string mineLayer = "";
        foreach (string layer in layerName)
        {
            if (LayerMask.LayerToName(gameObject.layer) == layer) {mineLayer = layer;}
        }
        layerName.Remove(mineLayer);
        _enemyLayer = LayerMask.GetMask(layerName.ToArray());
    }

    public void SetContext(
        unit unitData, skill skillData, GameObject selectedMark)
    {
        _unitData = unitData;
        _skillData = skillData;
        _selectedMark = selectedMark;

        if (_health != null) {_health.SetHp(_unitData.hp);}
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
    public Collider GetCollider() {return _collider;}
    public Health GetHealth() {return _health;}
    public bool GetSeleted() {return _selectedMark;}

    public List<GameObject> GetDetectedEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(
            _transform.position, _unitData.view_distance, _enemyLayer, QueryTriggerInteraction.Ignore);
        
        List<GameObject> gameObjects = new List<GameObject>();
        foreach (Collider hit in hits) {gameObjects.Add(hit.gameObject);}
        return gameObjects;
    }
}
