using UnityEngine;

[CreateAssetMenu(fileName = "building", menuName = "Scriptable Objects/building")]
public class building : ScriptableObject
{
    public int id;
    public string building_name;
    public float hp;
    public int[] spawn_unit_id;
    public int[] upgrade_skill_id;
}
