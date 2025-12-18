using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Database database;
    [SerializeField]
    private Unit[] units;
    [SerializeField]
    private MouseSelectArea selectArea;

    void Awake()
    {
        database = FindFirstObjectByType<Database>(FindObjectsInactive.Exclude);
        units = FindObjectsByType<Unit>(FindObjectsSortMode.None);

        foreach (Unit unit in units)
        {
            unit.db = database;
            selectArea.select += unit.CheckSelect;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
