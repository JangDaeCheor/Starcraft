using System;
using UnityEngine;

public class Database : MonoBehaviour
{
    [SerializeField]
    private unit[] unitDB;
    [SerializeField]
    private skill[] skillDB;
    [SerializeField]
    private building[] buildingDB;
    [SerializeField]
    public mouse mouseDB;
    [SerializeField]
    public ui_command commandDB;

    public event Action<Database> eSetData;
    public event Action<Database> eChangeData;

    public void SetData()
    {
        eSetData?.Invoke(this);
    }

    public unit SelectUnitName(string name)
    {
        foreach(unit unit in unitDB)
        {
            if (unit.name == name)
            {
                return unit;
            }
        }
        return null;
    }

    public unit SelectUnitId(int id)
    {
        foreach(unit unit in unitDB)
        {
            if (unit.id == id)
            {
                return unit;
            }
        }
        return null;
    }

    public skill SelectSkillName(string name)
    {
        foreach(skill skill in skillDB)
        {
            if (skill.name == name)
            {
                return skill;
            }
        }
        return null;
    }

    public skill SelectSkillId(int id)
    {
        foreach(skill skill in skillDB)
        {
            if (skill.id == id)
            {
                return skill;
            }
        }
        return null;
    }

    public building SelectBuildingName(string name)
    {
        foreach(building building in buildingDB)
        {
            if (building.name == name)
            {
                return building;
            }
        }
        return null;
    }

    public building SelectBuildingId(int id)
    {
        foreach(building building in buildingDB)
        {
            if (building.id == id)
            {
                return building;
            }
        }
        return null;
    }
}
