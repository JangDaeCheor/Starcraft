using System.Data.Common;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Database database;
    [SerializeField]
    private UnitManager unitManager;
    [SerializeField]
    private UIInterface ui;

    void Awake()
    {
        database = FindFirstObjectByType<Database>(FindObjectsInactive.Exclude);
        ui = FindFirstObjectByType<UIInterface>(FindObjectsInactive.Exclude);
        unitManager = FindFirstObjectByType<UnitManager>(FindObjectsInactive.Exclude);

        database.eSetData += unitManager.SetData;
        database.eSetData += ui.SetData;

        unitManager.eGetData += database.SetData;
        ui.eGetData += database.SetData;

        database.eChangeData += unitManager.ChangeData;
        database.eChangeData += ui.SetData;

        ui.eWorldClick += unitManager.MoveCommand;
        ui.eSelect += unitManager.CheckSelect;

        ui.eAttack += unitManager.AttackCommand;

        unitManager.eSelectedUnits += ui.SelectedEvent;
    }

    void Start()
    {
        database.SetData();
    }

    void Update()
    {
        ui.fsm.Tick(Time.deltaTime);
        unitManager.Tick(Time.deltaTime);
    }

    void FixedUpdate()
    {
        unitManager.FixedTick(Time.deltaTime);
    }
}
