using UnityEngine;


[CreateAssetMenu(fileName = "unit", menuName = "Scriptable Objects/unit")]
public class unit : ScriptableObject
{
    public int id;
    public string unit_name;
    public int hp;
    public int skill_id;
    public float move_speed = 1;
}
