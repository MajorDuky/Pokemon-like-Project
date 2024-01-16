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
    public bool isAlone;
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
        actualFightingMonster = monsters[0];
        return actualFightingMonster;
    }

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
                Debug.Log("Coucou");
                bm.enemyChoice = BattleManager.BattleChoice.Attack;
                bm.hasEnemyPlayed = true;
            }
            else
            {
                Debug.Log("Coucou2");
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
