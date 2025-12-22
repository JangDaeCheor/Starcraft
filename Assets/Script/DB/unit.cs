using UnityEngine;


[CreateAssetMenu(fileName = "unit", menuName = "Scriptable Objects/unit")]
public class unit : ScriptableObject
{
    public int id;
    public string unit_name;
    public float hp;
    public int skill_id;
    public float move_speed = 1;
    public float view_distance = 10.0f;
}
