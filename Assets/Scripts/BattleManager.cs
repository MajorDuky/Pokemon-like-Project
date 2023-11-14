using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private MonsterScriptableObject allyMonster;
    private MonsterScriptableObject enemyMonster;
    private bool isBattleOver;
    private int roundCounter;
    private List<MonsterScriptableObject> roundOrder;
    private bool isSuccess;
    [SerializeField] private BattleUIHandler ui;

    private void OnEnable()
    {
        isBattleOver = false;
        roundCounter = 0;
        roundOrder = new List<MonsterScriptableObject>();
    }

    private void Start()
    {
        
    }
    private void OnDisable()
    {
        roundOrder.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeBattle(MonsterScriptableObject ally, MonsterScriptableObject enemy)
    {
        allyMonster = ally;
        enemyMonster = enemy;
        DetermineRoundOrder();
        ui.InitializeUIBattle(ally, enemy);
    }

    private void DetermineRoundOrder()
    {
        roundOrder.Clear();
        if (allyMonster.speed >= enemyMonster.speed)
        {
            roundOrder.Add(allyMonster);
            roundOrder.Add(enemyMonster);
        }
        else
        {
            roundOrder.Add(enemyMonster);
            roundOrder.Add(allyMonster);
        }
    }

    private void HandleBattle()
    {
        if (!isBattleOver)
        {

        }
        else
        {
            EndBattle();
        }
    }

    private void EndBattle()
    {
        if (isSuccess)
        {
            // fermeture combat, prise xp
        }
        else
        {
            // tp joueur centre de soins, message battle over
        }
    }
}
