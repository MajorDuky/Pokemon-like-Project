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
    public float baseXPGain;
    [SerializeField] private float baseXPGainMultiplier;
    public Vector2 lastVisitedHealCenterPosition;
    public PNJHandler actualFightingPNJ;
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
        baseXPGainMultiplier = 1.2f;
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

    // Method that is part of the XP gain process, increases the number of KOed enemy's monster
    public void IncreaseKnockedOutMonsters()
    {
        knockedOutMonsters++;
    }

    // Method that is part of the XP gain process, resets the number of KOed enemy's monster
    public void ResetKnockedOutMonsters()
    {
        knockedOutMonsters = 0;
    }

    // Method that helps to determine if the player is out of monsters
    public void InspectMonstersPlayerLife()
    {
        int koMonsters = 0;
        foreach (var monster in playerTeam)
        {
            if (!monster.isAlive)
            {
                koMonsters++;
            }
        }

        isPlayerKO = koMonsters == playerTeam.Count;
    }

    // Method that is part of the XP gain process, increases the base XP gained at the end of a battle
    public void IncreaseBaseXPGain()
    {
        int currentMaxLvlMonster = 1;
        foreach (var monster in playerTeam)
        {
            currentMaxLvlMonster = currentMaxLvlMonster < monster.level ? monster.level : currentMaxLvlMonster;
        }
        baseXPGain = currentMaxLvlMonster * baseXPGainMultiplier;
    }

    // Method that is called at the end of a successful battle
    public void SuccessBattleEnd()
    {
        foreach (var monster in playerTeam)
        {
            monster.GainXp(baseXPGain * knockedOutMonsters);
            // ajout de messages dans une liste de messages à afficher
        }
        actualFightingPNJ.detectionCollider.enabled = false;
    }

    // Method that heals entirely the player's team
    public void MassHealPlayer()
    {
        foreach (var monster in playerTeam)
        {
            monster.Revive();
            monster.RefillSP();
        }
    }
}
