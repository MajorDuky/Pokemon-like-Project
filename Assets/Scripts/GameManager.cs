using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<PNJScriptableObject> pnjs;
    public List<BuildingScriptableObject> buildings;
    [SerializeField] private PNJHandler pnjHandler;
    [SerializeField] private BuildingManager buildingHandler;
    [SerializeField] private Transform worldObjectTransform;
    public List<MonsterScriptableObject> playerTeam;
    public BattleManager battleManager;
    // pense aux monstres

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;

        GeneratePNJ(pnjs);

        foreach (var building in buildings)
        {
            BuildingManager clone = Instantiate(buildingHandler, worldObjectTransform);
            clone.building = building;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartBattle(List<MonsterScriptableObject> enemyTeam)
    {
        battleManager.gameObject.SetActive(true);
        battleManager.InitializeBattle(enemyTeam);
    }

    public void GeneratePNJ(List<PNJScriptableObject> pnjsToSpawn)
    {
        foreach (var pnj in pnjsToSpawn)
        {
            PNJHandler clone = Instantiate(pnjHandler, pnj.spawnLocation, Quaternion.identity, worldObjectTransform);
            clone.dataPNJ = pnj;
        }
    }
}
