using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public MonsterScriptableObject allyMonster;
    public MonsterScriptableObject enemyMonster;
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
        Run = 3,
        Submission = 4
    }
    public BattleChoice allyChoice;
    public BattleChoice enemyChoice;
    public CapacityScriptableObject allyCapacity;
    public CapacityScriptableObject enemyCapacity;
    private List<MonsterScriptableObject> roundOrder;
    private bool isSuccess;
    private bool isSubmitted;
    public MonsterScriptableObject newPlayerMonster;
    public BattleUIHandler ui;
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
        allyMonster = GameManager.Instance.DetermineFirstLivingMonsterInPlayerTeam();
        GameManager.Instance.isInBattle = true;
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
            foreach (var monster in roundOrder)
            {
                // Submission by the ally
                if (allyChoice == BattleChoice.Submission && GameManager.Instance.isWildEncounter)
                {
                    isSubmitted = Random.Range(0f, 100f) <= enemyMonster.submissionRate;
                    if (isSubmitted)
                    {
                        // Delencher Anim Soumission Réussie
                    }
                    else
                    {
                        Debug.Log("FAIL");
                        ui.UpdateBattleText("You failed to submit the monster !");
                    }
                }
                else if (monster.isAlly && allyChoice == BattleChoice.Run || !monster.isAlly && enemyChoice == BattleChoice.Run)
                {
                    MonsterScriptableObject coward = monster.isAlly ? allyMonster : enemyMonster;
                    MonsterScriptableObject bold = !monster.isAlly ? allyMonster : enemyMonster;
                    RunAttempt(coward, bold);
                }
                else if (monster.isAlly && allyChoice == BattleChoice.Switch || !monster.isAlly && enemyChoice == BattleChoice.Switch)
                {
                    MonsterScriptableObject switcher = monster.isAlly ? allyMonster : enemyMonster;
                    Switch(switcher);
                }
                else
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
                // Constat des morts, switch si nécessaire + gestion game over
                bool endBattle = ActionEndChecks();
                if (endBattle)
                {
                    break;
                }

                enemyBehavior.DetermineLowHPState();
                if (enemyBehavior.isLowHP && GameManager.Instance.isWildEncounter && !ui.submissionUI.gameObject.activeInHierarchy)
                {
                    ui.HandleSubmissionUIDisplay();
                }
            }
            // Afterglow
            hasAllyPlayed = false;
            hasEnemyPlayed = false;
            // test valeurs HP + conditions sur état de la team : game over si tous les alliés = KO / win si tous les ennemis = KO
            roundCounter++;

            if (isSubmitted)
            {
                isSuccess = true;
                // PENSE A LA LIMITE DE MONSTRES DANS LA TEAM (ENVOYER DANS LA BANQUE QUAND PLEIN)
                GameManager.Instance.playerTeam.Add(enemyMonster);
                enemyMonster.isAlly = true;
                GameManager.Instance.currentSpawner.encounterableMonsters.Remove(enemyMonster);
                EndBattle();
            }
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
            GameManager.Instance.isInBattle = false;
            ui.ExitBattle();
        }
    }

    /// <summary>
    /// Method that is called when the player or the enemy wants to switch monster
    /// </summary>
    /// <param name="switchedMonster">The monster to switch</param>
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
            if (newMonster.isAlive && !GameManager.Instance.isEnemyKO)
            {
                enemyMonster = newMonster;
                ui.EnemySwitch(enemyMonster);
            }
        }
    }

    /// <summary>
    /// Method that is called at the end of the player and the enemy's actions.
    /// Determines if a battle has to end. Succesfully or not.
    /// </summary>
    /// <returns>Boolean : returns true when the player or the enemy is out of monsters</returns>
    private bool ActionEndChecks()
    {
        bool isAnOpponentKO = false;
        if (!allyMonster.isAlive || !enemyMonster.isAlive)
        {
            if (!enemyMonster.isAlive)
            {
                Switch(enemyMonster);
            }

            GameManager.Instance.InspectMonstersPlayerLife();
            if (!GameManager.Instance.isPlayerKO && !allyMonster.isAlive)
            {
                ui.UpdateBattleText("Your monster is KO, switch with an other !");
                ui.UseTeamButton();
                ui.HandleInteractableButtons();
            }

            if (GameManager.Instance.isEnemyKO)
            {
                isAnOpponentKO = true;
                isSuccess = true;
                EndBattle();
            }
            else if (GameManager.Instance.isPlayerKO)
            {
                isAnOpponentKO = true;
                isSuccess = false;
                EndBattle();
            }
        }
        return isAnOpponentKO;
    }

    // Method that handles the end of a battle for the battle manager
    private void EndBattle()
    {
        GameManager.Instance.isInBattle = false;
        isSubmitted = false;
        if (GameManager.Instance.isWildEncounter)
        {
            ui.HandleSubmissionUIDisplay();
        }
        if (isSuccess)
        {
            GameManager.Instance.SuccessBattleEnd();
            GameManager.Instance.knockedOutMonsters = 0;
            if (GameManager.Instance.isWildEncounter)
            {
                enemyMonster.Revive();
                enemyMonster.RefillSP();
            }
            // trigger corout pour afficher au fur et à mesure les messages de la liste
            gameObject.SetActive(false);
        }
        else
        {
            playerMovement.transform.position = GameManager.Instance.lastVisitedHealCenterPosition;
            playerMovement.TPMovePoint();
            GameManager.Instance.MassHealPlayer();
            foreach (var monster in enemyBehavior.enemyTeam)
            {
                monster.Revive();
                monster.RefillSP();
            }
            GameManager.Instance.LaunchGameOverSequence();
            GameManager.Instance.knockedOutMonsters = 0;
            gameObject.SetActive(false);
        }
    }
}
