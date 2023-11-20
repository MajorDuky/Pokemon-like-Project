using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleBehavior : MonoBehaviour
{
    public List<MonsterScriptableObject> enemyTeam = new List<MonsterScriptableObject>();
    public MonsterScriptableObject actualFightingMonster;
    [SerializeField] private BattleManager bm;
    private bool hasSufficientSP;
    private List<CapacityScriptableObject> usableCapacities = new List<CapacityScriptableObject>();
    private bool isLowHP;
    private bool isAlone;
    private bool isAllTeamInDanger;

    // Update is called once per frame
    void Update()
    {
        
    }

    public MonsterScriptableObject InitializeBattle(List<MonsterScriptableObject> monsters)
    {
        foreach (var monster in monsters)
        {
            enemyTeam.Add(monster);
        }

        if (enemyTeam.Count == 1)
        {
            isAlone = true;
        }

        return actualFightingMonster = monsters[0];
    }

    public void MakeChoice()
    {
        DetermineSufficientSP(actualFightingMonster.capacitiesList);
        DetermineLowHPState();
        if (!isAlone)
        {
            DetermineIsAllTeamInDanger();
        }
        
        if (hasSufficientSP && !isLowHP)
        {
            LaunchCapacitySequence();
        }
        else if (isLowHP && !isAlone)
        {
            if (isAllTeamInDanger && hasSufficientSP)
            {
                LaunchCapacitySequence();
            }
            else if (isAllTeamInDanger && !hasSufficientSP)
            {
                bm.enemyChoice = BattleManager.BattleChoice.Attack;
                bm.hasEnemyPlayed = true;
            }
            else
            {
                bm.enemyChoice = BattleManager.BattleChoice.Switch;
                // switch
                bm.hasEnemyPlayed = true;
            }

        }
        else if (hasSufficientSP)
        {
            LaunchCapacitySequence();
        }
        else
        {
            bm.enemyChoice = BattleManager.BattleChoice.Attack;
            bm.hasEnemyPlayed = true;
        }
    }

    public void DetermineSufficientSP(List<CapacityScriptableObject> availableCapacities)
    {
        usableCapacities.Clear();
        foreach (var capacity in availableCapacities)
        {
            if (capacity.spValue <= actualFightingMonster.spiritPower)
            {
                usableCapacities.Add(capacity);
            }
        }
        hasSufficientSP = usableCapacities.Count > 0;
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

    private void LaunchCapacitySequence()
    {
        CapacityScriptableObject capacityToLaunch = usableCapacities[Random.Range(0, usableCapacities.Count)];
        bm.enemyChoice = BattleManager.BattleChoice.Capacity;
        bm.enemyCapacity = capacityToLaunch;
        bm.hasEnemyPlayed = true;
    }
}
