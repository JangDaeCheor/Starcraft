using UnityEngine;

[CreateAssetMenu(fileName = "skill", menuName = "Scriptable Objects/skill")]
public class skill : ScriptableObject
{
    public int id;
    public string skill_name;
    public int damage;
}
