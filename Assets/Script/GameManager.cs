using System.Data.Common;
using UnityEditor.VersionControl;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Database database;
    [SerializeField]
    private UnitManager unitManager;
    [SerializeField]
    private UIInterface ui;

    [SerializeField]
    private Building _building;

    void Awake()
    {
        database = FindFirstObjectByType<Database>(FindObjectsInactive.Exclude);
        ui = FindFirstObjectByType<UIInterface>(FindObjectsInactive.Exclude);
        unitManager = FindFirstObjectByType<UnitManager>(FindObjectsInactive.Exclude);

        ui.eWorldRightClick += unitManager.MoveCommand;
        ui.eSelect += unitManager.CheckSelect;

        ui.eAttack += unitManager.AttackCommand;

        unitManager.eSelectedUnits += ui.SelectedUnitEvent;
        _building.eBuildingCommand += ui.SelectedBuildingEvent;


        // 임시
        ui.eSpawnUnit += _building.SpawnUnit;
        ui.eWorldLeftClick += _building.CheckId;

        _building.eSpawnUnit += unitManager.SpawnUnit;
    }

    void Start()
    {
        MessageBus.Publish<mSetData>(new mSetData(database));
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
