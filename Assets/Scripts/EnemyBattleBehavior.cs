using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleBehavior : MonoBehaviour
{
    public List<MonsterScriptableObject> enemyTeam = new List<MonsterScriptableObject>();
    public MonsterScriptableObject actualFightingMonster;
    [SerializeField] private BattleManager bm;
    private bool hasSufficientSP;
    private bool isLowHP;
    private bool isAlone;
    private bool isAllTeamInDanger;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeBattle(List<MonsterScriptableObject> monsters)
    {
        foreach (var monster in monsters)
        {
            enemyTeam.Add(monster);
        }

        if (enemyTeam.Count == 1)
        {
            isAlone = true;
        }
    }

    public void MakeChoice()
    {
        if (hasSufficientSP && !isLowHP)
        {
            // lancer capacité
        }
        else if (isLowHP && !isAlone)
        {
            // switch
        }
        else if (hasSufficientSP)
        {
            // lancer capacité
        }
        else
        {
            // lancer attaque basique
        }
    }

    public void DetermineSufficientSP(List<CapacityScriptableObject> availableCapacities)
    {
        int usableCapacities = 0;
        foreach (var capacity in availableCapacities)
        {
            usableCapacities = capacity.spValue <= actualFightingMonster.spiritPower ? usableCapacities++ : usableCapacities;
        }
        hasSufficientSP = usableCapacities > 0;
    }

    public void DetermineLowHPState()
    {
        float lowHPValue = actualFightingMonster.maxHealth / 5;
        isLowHP = actualFightingMonster.health <= lowHPValue;
    }

    public void DetermineIsAllTeamInDanger()
    {
        int inDangerCounter = 0;
        foreach (var monster in enemyTeam)
        {
            float lowHPValue = monster.maxHealth / 5;
            inDangerCounter = actualFightingMonster.health <= lowHPValue ? inDangerCounter++ : inDangerCounter;
        }
        isAllTeamInDanger = inDangerCounter == enemyTeam.Count;
    }
}
