using UnityEngine;

[CreateAssetMenu(fileName = "mouse", menuName = "Scriptable Objects/mouse")]
public class mouse : ScriptableObject
{
    public GameObject right_click_effect;
    public GameObject attack_effect;
    public LayerMask ground;
    public float left_mark_time;
}
