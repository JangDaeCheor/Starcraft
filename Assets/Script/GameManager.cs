using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Database database;
    [SerializeField]
    private Unit[] units;
    [SerializeField]
    private UIInterface ui;

    void Awake()
    {
        database = GetComponent<Database>();
        // database = FindFirstObjectByType<Database>(FindObjectsInactive.Exclude);
        units = FindObjectsByType<Unit>(FindObjectsSortMode.None);
        ui = FindFirstObjectByType<UIInterface>(FindObjectsInactive.Exclude);

        foreach (Unit unit in units)
        {
            unit.db = database;
        }

        ui.db = database;
    }

    void Start()
    {
        foreach( Unit unit in units)
        {
            ui.select += unit.CheckSelect;
            ui.mapClick += unit.MoveTo;
        }
    }

    void Update()
    {
        ui.Tick(Time.deltaTime);
    }

    void FixedUpdate()
    {
        foreach(Unit unit in units)
        {
            unit.FixedTick(Time.fixedDeltaTime);
        }
    }
}
