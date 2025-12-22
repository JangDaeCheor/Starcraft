using UnityEngine;

[CreateAssetMenu(fileName = "skill", menuName = "Scriptable Objects/skill")]
public class skill : ScriptableObject
{
    public int id;
    public string skill_name;
    public float damage;
    public float cooltime = 1.0f;
    public float attack_distance = 1.0f;
    public GameObject effect;
}
