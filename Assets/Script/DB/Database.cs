using System;
using UnityEngine;

public class Database : MonoBehaviour
{
    [SerializeField]
    private unit[] unitDB;
    [SerializeField]
    private skill[] skillDB;
    [SerializeField]
    public mouse mouseDB;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
