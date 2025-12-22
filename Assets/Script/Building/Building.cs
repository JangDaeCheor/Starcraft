using System;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public enum State
    {
        Upgrade,
        Spawn
    }
    [SerializeField]
    private State state = State.Spawn;

    [SerializeField]
    public int id;
    [SerializeField]
    private string buildingName;
    [SerializeField]
    private float spawnPosOffset = 0.5f;
    [SerializeField]
    private int spawnRoundCount = 5;
    private int spawnCount = 0;
    [SerializeField]
    private bool isSelected = false;

    private building _buildingData;

    private Collider _collider;

    private List<unit> _spawnPossibleUnits = new List<unit>();
    private List<skill> _upgradePossibleSkills = new List<skill>();

    public event Action<unit[], skill[]> eBuildingCommand;
    public event Action<GameObject> eSpawnUnit;

    // 임시...
    public void CheckId(GameObject go)
    {
        Building clickBuilding = go.GetComponent<Building>();
        if (clickBuilding != null)
        {
            if (id == clickBuilding.id)
            {
                SetSelected(true);
            }
        }
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        
        if (isSelected)
        {
            eBuildingCommand?.Invoke(_spawnPossibleUnits.ToArray(), _upgradePossibleSkills.ToArray());
        }
    }

    public void SetData(Database db)
    {
        _buildingData = db.SelectBuildingName(buildingName);
        foreach (int unitId in _buildingData.spawn_unit_id)
        {
            _spawnPossibleUnits.Add(db.SelectUnitId(unitId));
        }
        foreach (int skillId in _buildingData.upgrade_skill_id)
        {
            _upgradePossibleSkills.Add(db.SelectSkillId(skillId));
        }
    }

    public void SpawnUnit(GameObject prefab)
    {
        // 한바퀴돌면 겹쳐서 생성됨...
        float angle = spawnCount * (360.0f / spawnRoundCount);
        Vector3 dir = Quaternion.Euler(0, angle, 0) * Vector3.forward;
        Vector3 surface = _collider.ClosestPoint(_collider.bounds.center + dir * 20f);
        Vector3 spawnPos = surface + dir * spawnPosOffset;

        GameObject go = Instantiate(prefab, spawnPos, prefab.transform.rotation);
        spawnCount += 1;

        eSpawnUnit?.Invoke(go);
    }

    void Awake()
    {
        _collider = GetComponent<Collider>();
    }
}
