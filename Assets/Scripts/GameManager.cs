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
    [SerializeField] private Transform pnjObjectTransform;
    [SerializeField] private Transform buildingObjectTransform;
    public List<MonsterScriptableObject> playerTeam;
    public BattleManager battleManager;
    public bool isPlayerKO;
    public bool isEnemyKO;
    public int knockedOutMonsters;
    // pense aux monstres

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        isPlayerKO = false;
        isEnemyKO = false;
        GeneratePNJ(pnjs);

        foreach (var building in buildings)
        {
            BuildingManager clone = Instantiate(buildingHandler, buildingObjectTransform);
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
            PNJHandler clone = Instantiate(pnjHandler, pnj.spawnLocation, Quaternion.identity, pnjObjectTransform);
            clone.dataPNJ = pnj;
        }
    }

    public void HandlePNJAndBuildingDisplay()
    {
        if (pnjObjectTransform.gameObject.activeInHierarchy && buildingObjectTransform.gameObject.activeInHierarchy)
        {
            pnjObjectTransform.gameObject.SetActive(false);
            buildingObjectTransform.gameObject.SetActive(false);
        }
        else
        {
            pnjObjectTransform.gameObject.SetActive(true);
            buildingObjectTransform.gameObject.SetActive(true);
        }
    }

    public void IncreaseKnockedOutMonsters()
    {
        knockedOutMonsters++;
    }

    public void ResetKnockedOutMonsters()
    {
        knockedOutMonsters = 0;
    }

    public void InspectMonstersPlayerLife()
    {
        int koMonsters = 0;
        foreach (var monster in playerTeam)
        {
            koMonsters = !monster.isAlive ? koMonsters++ : koMonsters;
        }

        isPlayerKO = koMonsters == playerTeam.Count;
    }
}
