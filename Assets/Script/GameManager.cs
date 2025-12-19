using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Database database;
    [SerializeField]
    private Unit[] units;
    [SerializeField]
    private UIInterface ui;

    void Test()
    {
        Debug.Log("test");
    }

    // 찾는 것은 Awake에서..
    void Awake()
    {
        database = FindFirstObjectByType<Database>(FindObjectsInactive.Exclude);
        units = FindObjectsByType<Unit>(FindObjectsSortMode.None);
        ui = FindFirstObjectByType<UIInterface>(FindObjectsInactive.Exclude);

        ui.db = database;

        foreach (Unit unit in units)
        {
            unit.db = database;
        }
    }

    // 이벤트 연결은 start에서..
    void Start()
    {
        ui.eAttack += Test;
        foreach( Unit unit in units)
        {
            ui.eSelect += unit.CheckSelect;
            ui.eMapClick += unit.MoveTo;
        }
    }

    void Update()
    {
        ui.Tick(Time.deltaTime);

        foreach(Unit unit in units)
        {
            unit.Tick(Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        foreach(Unit unit in units)
        {
            unit.FixedTick(Time.fixedDeltaTime);
        }
    }
}
