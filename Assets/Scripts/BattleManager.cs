using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private MonsterScriptableObject allyMonster;
    private MonsterScriptableObject enemyMonster;
    private bool isBattleOver;
    private int roundCounter;
    public bool hasAllyPlayed;
    public bool hasEnemyPlayed;
    [SerializeField] private PlayerMovement playerMovement;
    public enum BattleChoice
    {
        Attack = 0,
        Capacity = 1,
        Switch = 2,
        Run = 3
    }
    public BattleChoice allyChoice;
    public BattleChoice enemyChoice;
    public CapacityScriptableObject allyCapacity;
    public CapacityScriptableObject enemyCapacity;
    private List<MonsterScriptableObject> roundOrder;
    private bool isSuccess;
    public MonsterScriptableObject newPlayerMonster;
    [SerializeField] private BattleUIHandler ui;
    [SerializeField] private EnemyBattleBehavior enemyBehavior;
    [SerializeField] private CapacityScriptableObject defaultCapacity;

    private void OnEnable()
    {
        isBattleOver = false;
        roundCounter = 0;
        roundOrder = new List<MonsterScriptableObject>();
        hasAllyPlayed = false;
        hasEnemyPlayed = false;
        playerMovement.isInBattle = true;
    }

    private void Start()
    {
        
    }

    private void OnDisable()
    {
        roundOrder.Clear();
        foreach (var monster in GameManager.Instance.playerTeam)
        {
            monster.RefillSP();
        }
        playerMovement.isInBattle = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAllyPlayed && hasEnemyPlayed)
        {
            HandleBattle();
        }
        else if (hasAllyPlayed && !hasEnemyPlayed)
        {
            enemyBehavior.MakeChoice();
        }
    }

    /// <summary>
    /// Method that initializes the battle : defines the ally monster, the enemy monster, the round order and initializes the UI
    /// </summary>
    /// <param name="ally">The ally's monster</param>
    /// <param name="enemyTeam">The enemy's monster team</param>
    public void InitializeBattle(List<MonsterScriptableObject> enemyTeam)
    {
        allyMonster = GameManager.Instance.playerTeam[0];
        enemyMonster = enemyBehavior.InitializeBattle(enemyTeam);
        DetermineRoundOrder();
        ui.InitializeUIBattle(allyMonster, enemyMonster);
    }

    // Method that determines the round order based on the opponents speed
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

    // Main method from the battle manager. It handles the battle, based on the CPU and the player's choices
    private void HandleBattle()
    {
        if (!isBattleOver)
        {
            if (allyChoice == BattleChoice.Run || enemyChoice == BattleChoice.Run)
            {
                MonsterScriptableObject coward = allyChoice == BattleChoice.Run ? allyMonster : enemyMonster;
                MonsterScriptableObject bold = allyChoice != BattleChoice.Run ? allyMonster : enemyMonster;
                RunAttempt(coward, bold);
            }
            else if (allyChoice == BattleChoice.Switch || enemyChoice == BattleChoice.Switch)
            {
                MonsterScriptableObject switcher = allyChoice == BattleChoice.Switch ? allyMonster : enemyMonster;
                Switch(switcher);
            }
            else
            {
                foreach (var monster in roundOrder)
                {
                    if (monster.isAlly)
                    {
                        float dmgValue = AttackOpponent(allyMonster, enemyMonster, allyChoice);
                        if (dmgValue > 0)
                        {
                            ui.UpdateEnemyHP(enemyMonster.health, enemyMonster.maxHealth);
                        }
                        if (allyChoice == BattleChoice.Capacity)
                        {
                            ui.UpdateAllySP(allyMonster.spiritPower, allyMonster.maxSpiritPower);
                        }
                    }
                    else
                    {
                        float dmgValue = AttackOpponent(enemyMonster, allyMonster, enemyChoice);
                        if (dmgValue > 0)
                        {
                            ui.UpdateAllyHP(allyMonster.health, allyMonster.maxHealth);
                        }
                        if (enemyChoice == BattleChoice.Capacity)
                        {
                            ui.UpdateEnemySP(enemyMonster.spiritPower, enemyMonster.maxSpiritPower);
                        }
                    }
                }
            }

            // Constat des morts, switch si nécessaire + gestion game over
            if (!allyMonster.isAlive || !enemyMonster.isAlive)
            {
                if (!enemyMonster.isAlive)
                {
                    Switch(enemyMonster);
                }
                if (GameManager.Instance.isEnemyKO)
                {
                    // Victoire allié
                    isBattleOver = true;
                    // gagner xp
                    // sortir combat
                }
                else
                {
                    GameManager.Instance.InspectMonstersPlayerLife();
                    if (!GameManager.Instance.isPlayerKO)
                    {
                        ui.UpdateBattleText("Your monster is KO, switch with an other !");
                        ui.UseTeamButton();
                        ui.HandleInteractableButtons();
                    }
                    else
                    {
                        // Défaite allié
                        isBattleOver = true;
                        // sortir combat
                        // TP centre soins
                    }
                }
            }
            // Afterglow
            hasAllyPlayed = false;
            hasEnemyPlayed = false;
            // test valeurs HP + conditions sur état de la team : game over si tous les alliés = KO / win si tous les ennemis = KO
            roundCounter++;
        }
        else
        {
            EndBattle();
        }
    }

    /// <summary>
    /// Method that calculates the damages taken by an opponent based on the battle choice and the chosen capacity
    /// </summary>
    /// <param name="caster">The one launching the capacity</param>
    /// <param name="receiver">The one receiving the attack</param>
    /// <param name="choice">Battle choice that determines if it's a basic attack or a capacity</param>
    /// <returns>Float : damages taken by the opponent</returns>
    private float AttackOpponent(MonsterScriptableObject caster, MonsterScriptableObject receiver, BattleChoice choice)
    {
        CapacityScriptableObject capacityToLaunch;
        float damageModifier = 1;
        switch (choice)
        {
            case BattleChoice.Attack:
                capacityToLaunch = defaultCapacity;
                Debug.Log(caster + " " + capacityToLaunch);
                break;
            case BattleChoice.Capacity:
                capacityToLaunch = caster.isAlly ? allyCapacity : enemyCapacity;
                Debug.Log(caster + " " + capacityToLaunch);
                break;
            default:
                capacityToLaunch = defaultCapacity;
                Debug.Log(caster + " " + capacityToLaunch);
                break;
        }
        foreach (var type in receiver.weaknessesList)
        {
            if (capacityToLaunch.type == type)
            {
                damageModifier *= 1.5f;
            }
        }
        foreach (var type in receiver.strengthsList)
        {
            if (capacityToLaunch.type == type)
            {
                damageModifier *= 0.5f;
            }
        }
        if (receiver.ignoreDmgList.Count > 0)
        {
            foreach (var type in receiver.ignoreDmgList)
            {
                damageModifier = 0f;
            }
        }

        float dmgValue = capacityToLaunch.strength * damageModifier;
        receiver.TakeDamage(dmgValue);
        caster.ReduceSP(capacityToLaunch.spValue);
        return dmgValue;
    }

    // Method that determines if a monster can run away from the battle
    private void RunAttempt(MonsterScriptableObject coward, MonsterScriptableObject bold)
    {
        if (coward.speed >= bold.speed)
        {
            string determinant = coward.isAlly ? "You" : "The enemy";
            ui.UpdateBattleText($"{determinant} managed to run away !");
            ui.ExitBattle();
        }
    }

    public void Switch(MonsterScriptableObject switchedMonster)
    {
        if (switchedMonster.isAlly)
        {
            allyMonster = newPlayerMonster;
            ui.AllySwitch(allyMonster);
        }
        else
        {
            MonsterScriptableObject newMonster = enemyBehavior.SwitchOrDie();
            if (enemyMonster.monsterName != newMonster.monsterName)
            {
                enemyMonster = newMonster;
                ui.EnemySwitch(enemyMonster);
            }
        }
    }

    // Method that handles the end of a battle for the battle manager
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
