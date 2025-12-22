using UnityEngine;
using UnityEngine.Rendering;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float maxHp;
    [SerializeField]
    private float currentHp;

    public void HitDamage(float damage)
    {
        currentHp -= damage;
        Debug.Log($"{gameObject.name} : {currentHp}");
        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetHp(float hp)
    {
        maxHp = hp;
        currentHp = hp;
    }
}
