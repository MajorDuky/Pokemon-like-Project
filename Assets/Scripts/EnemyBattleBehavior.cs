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
    public bool isLowHP;
    public bool isAlone;
    private bool isAllTeamInDanger;

    private void OnDisable()
    {
        enemyTeam.Clear();
    }

    public MonsterScriptableObject InitializeBattle(List<MonsterScriptableObject> monsters)
    {
        foreach (var monster in monsters)
        {
            enemyTeam.Add(monster);
            if(!monster.hasBeenEncountered)
            {
                monster.RegisterMonster("Untraceable");
            }
        }

        if (enemyTeam.Count == 1)
        {
            isAlone = true;
        }
        actualFightingMonster = monsters[0];
        return actualFightingMonster;
    }

    // Main algorithm that determines what the enemy should do during a battle
    public void MakeChoice()
    {
        DetermineSufficientSP(actualFightingMonster.capacitiesList);
        DetermineLowHPState();
        DetermineIsAloneState();
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

    /// <summary>
    /// Method that helps the main algorithm takes its decision.
    /// This one helps determine if the enemy has enough SP to launch a capacity
    /// </summary>
    /// <param name="availableCapacities">List of the enemy's capacities</param>
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

    /// <summary>
    /// Method that helps the main algorithm takes its decision.
    /// This one helps determine if the enemy's actual fighting monster's HP are low
    /// </summary>
    public void DetermineLowHPState()
    {
        float lowHPValue = actualFightingMonster.maxHealth / 5;
        isLowHP = actualFightingMonster.health <= lowHPValue;
    }

    /// <summary>
    /// Method that helps the main algorithm takes its decision.
    /// This one helps determine if the enemy's monsters are all low in HP
    /// </summary>
    public void DetermineIsAllTeamInDanger()
    {
        int inDangerCounter = 0;
        foreach (var monster in enemyTeam)
        {
            float lowHPValue = monster.maxHealth / 5;
            if (monster.health <= lowHPValue)
            {
                inDangerCounter++;
            }
        }
        isAllTeamInDanger = inDangerCounter == enemyTeam.Count;
    }

    // Method that is called when the main algorithm decides to launch a capacity
    private void LaunchCapacitySequence()
    {
        CapacityScriptableObject capacityToLaunch = usableCapacities[Random.Range(0, usableCapacities.Count)];
        bm.enemyChoice = BattleManager.BattleChoice.Capacity;
        bm.enemyCapacity = capacityToLaunch;
        bm.hasEnemyPlayed = true;
    }

    // Method that switches monster and help the GameManager to calculate the final XP gain by increasing the knockedOutMonsters counter.
    public MonsterScriptableObject SwitchOrDie()
    {
        List<MonsterScriptableObject> aliveMonsters = new List<MonsterScriptableObject>();
        foreach (var monster in enemyTeam)
        {
            if(monster.isAlive)
            {
                aliveMonsters.Add(monster);
            }
        }

        if (aliveMonsters.Count == 0)
        {
            GameManager.Instance.IncreaseKnockedOutMonsters();
            GameManager.Instance.isEnemyKO = true;
        }
        else
        {
            if (!actualFightingMonster.isAlive)
            {
                GameManager.Instance.IncreaseKnockedOutMonsters();
            }
            MonsterScriptableObject monsterToSwitchWith = aliveMonsters[Random.Range(0, aliveMonsters.Count)];
            actualFightingMonster = monsterToSwitchWith;
        }

        return actualFightingMonster;
    }

    /// <summary>
    /// Method that helps the main algorithm takes its decision.
    /// This one helps determine if the enemy has only one monster remaining
    /// </summary>
    private void DetermineIsAloneState()
    {
        if (enemyTeam.Count > 1)
        {
            int isKO = 0;
            foreach (var monster in enemyTeam)
            {
                if (!monster.isAlive)
                {
                    isKO++;
                }
            }
            Debug.Log(isKO);
            isAlone = isKO == enemyTeam.Count - 1;
        }
        else
        {
            isAlone = true;
        }
    }
}
