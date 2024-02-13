using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;
using Unity.Mathematics;

public class BattleUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text allyNameText;
    [SerializeField] private TMP_Text enemyNameText;
    [SerializeField] private Slider allyHPSlider;
    [SerializeField] private TMP_Text allyHPText;
    [SerializeField] private Slider enemyHPSlider;
    [SerializeField] private TMP_Text enemyHPText;
    [SerializeField] private Slider allySPSlider;
    [SerializeField] private TMP_Text allySPText;
    [SerializeField] private Slider enemySPSlider;
    [SerializeField] private TMP_Text enemySPText;
    [SerializeField] private TMP_Text allyLevelText;
    [SerializeField] private TMP_Text enemyLevelText;
    [SerializeField] private Slider allyXPSlider;
    [SerializeField] private Image allySprite;
    [SerializeField] private Image enemySprite;
    [SerializeField] private TMP_Text battleText;
    [SerializeField] private GameObject actionButtonsHolder;
    [SerializeField] private GameObject capacityArea;
    [SerializeField] private GameObject capacitiesContainer;
    [SerializeField] private GameObject capacityItemPrefab;
    [SerializeField] private BattleManager bm;
    [SerializeField] private Transform teamUI;
    [SerializeField] private Button returnButton;
    public enum UITypes
    {
        Classic = 0,
        Capacity = 1,
        Team = 2,
        Attack = 3,
        Run = 4
    }
    public UITypes actualUIDisplayed;

    private void OnEnable()
    {
        actualUIDisplayed = UITypes.Classic;
        battleText.text = "Choose your next move.";
    }


    // Modifies the ally name text in the battle UI
    public void UpdateAllyName(string name)
    {
        allyNameText.text = name;
    }

    // Modifies the enemy name text in the battle UI
    public void UpdateEnemyName(string name)
    {
        enemyNameText.text = name;
    }

    // Modifies the ally HP value text in the battle UI
    public void UpdateAllyHP(float newAmount, float maxHP)
    {

        allyHPText.text = $"{Math.Floor(newAmount).ToString()} / {Math.Floor(maxHP).ToString()}";
        allyHPSlider.value = newAmount / maxHP;
    }

    // Modifies the enemy HP value text in the battle UI
    public void UpdateEnemyHP(float newAmount, float maxHP)
    {
        enemyHPText.text = $"{Math.Floor(newAmount).ToString()} / {Math.Floor(maxHP).ToString()}";
        enemyHPSlider.value = newAmount / maxHP;
    }

    // Modifies the ally SP value text in the battle UI
    public void UpdateAllySP(float newAmount, float maxSP)
    {
        allySPText.text = $"{Math.Floor(newAmount).ToString()} / {Math.Floor(maxSP).ToString()}";
        allySPSlider.value = newAmount / maxSP;
    }

    // Modifies the enemy SP value text in the battle UI
    public void UpdateEnemySP(float newAmount, float maxSP)
    {
        enemySPText.text = $"{Math.Floor(newAmount).ToString()} / {Math.Floor(maxSP).ToString()}";
        enemySPSlider.value = newAmount / maxSP;
    }

    // Modifies the ally XP value text in the battle UI
    public void UpdateAllyXP(float newAmount, float maxXP)
    {
        allyXPSlider.value = newAmount / maxXP;
    }

    // Modifies the ally sprite in the battle UI
    public void UpdateAllySprite(Sprite sprite)
    {
        allySprite.sprite = sprite;
    }

    // Modifies the enemy sprite in the battle UI
    public void UpdateEnemySprite(Sprite sprite)
    {
        enemySprite.sprite = sprite;
    }

    // Modifies the ally level in the battle UI
    public void UpdateAllyLevel(int level)
    {
        allyLevelText.text = level.ToString();
    }

    // Modifies the enemy level in the battle UI
    public void UpdateEnemyLevel(int level)
    {
        enemyLevelText.text = level.ToString();
    }

    // Modifies the battle text log in the battle UI
    public void UpdateBattleText(string text)
    {
        battleText.text = text;
    }

    // Display the available capacities in the Capacity Area
    public void UseCapacityButton()
    {
        actualUIDisplayed = UITypes.Capacity;
        battleText.text = "Choose the capacity you want to use.";
        FillCapacityArea(GameManager.Instance.battleManager.allyMonster.capacitiesList);
        HandleActiveActionsButtons();
        capacityArea.SetActive(true);
    }

    // Display the confirm prompt to launch a basic attack
    public void UseAttackButton()
    {
        actualUIDisplayed = UITypes.Attack;
        battleText.text = "Do you want to use a basic attack ?";
        HandleActiveActionsButtons();
    }
    
    // Display the team menu
    public void UseTeamButton()
    {
        actualUIDisplayed = UITypes.Team;
        battleText.text = "Which monster do you want to use ?";
        teamUI.gameObject.SetActive(true);
        HandleActiveActionsButtons();
    }

    // Display the confirm prompt to launch a run away sequence
    public void UseRunButton()
    {
        actualUIDisplayed = UITypes.Run;
        battleText.text = "Do you really want to run away ?";
        HandleActiveActionsButtons();
    }

    // Called when the user press a return button in the UI, behavior varies with the UI currently displayed
    public void UseReturnButton()
    {
        switch (actualUIDisplayed)
        {
            case UITypes.Capacity:
                capacityArea.SetActive(false);
                battleText.text = "Choose your next move.";
                HandleActiveActionsButtons();
                actualUIDisplayed = UITypes.Classic;
                break;
            case UITypes.Team:
                teamUI.gameObject.SetActive(false);
                battleText.text = "Choose your next move.";
                HandleActiveActionsButtons();
                actualUIDisplayed = UITypes.Classic;
                break;
            case UITypes.Attack:
                battleText.text = "Choose your next move.";
                HandleActiveActionsButtons();
                actualUIDisplayed = UITypes.Classic;
                break;
            case UITypes.Run:
                battleText.text = "Choose your next move.";
                HandleActiveActionsButtons();
                actualUIDisplayed = UITypes.Classic;
                break;
            default:
                break;
        }
    }

    // Called when the user press a confirm button in the UI, behavior varies with the UI currently displayed
    public void UseConfirmButton()
    {
        switch (actualUIDisplayed)
        {
            case UITypes.Attack:
                HandleActiveActionsButtons();
                bm.allyChoice = BattleManager.BattleChoice.Attack;
                bm.hasAllyPlayed = true;
                actualUIDisplayed = UITypes.Classic;
                break;
            case UITypes.Run:
                HandleActiveActionsButtons();
                bm.allyChoice = BattleManager.BattleChoice.Run;
                bm.hasAllyPlayed = true;
                actualUIDisplayed = UITypes.Classic;
                break;
            case UITypes.Team:
                HandleActiveActionsButtons();
                if(!returnButton.interactable)
                {
                    returnButton.interactable = true;
                }
                actualUIDisplayed = UITypes.Classic;
                break;
        }
    }

    // Handles the active state of the user's actions in the UI
    public void HandleActiveActionsButtons()
    {
        for (int i = 0; i < actionButtonsHolder.transform.childCount; i++)
        {
            if (actionButtonsHolder.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                actionButtonsHolder.transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                if (actualUIDisplayed == UITypes.Attack && actionButtonsHolder.transform.GetChild(i).gameObject.CompareTag("ConfirmAction") || 
                    actualUIDisplayed == UITypes.Run && actionButtonsHolder.transform.GetChild(i).gameObject.CompareTag("ConfirmAction"))
                {
                    actionButtonsHolder.transform.GetChild(i).gameObject.SetActive(true);
                }
                
                if(actionButtonsHolder.transform.GetChild(i).gameObject.CompareTag("ClassicActions"))
                {
                    actionButtonsHolder.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }

    // Handles the interactability of the displayed buttons
    public void HandleInteractableButtons()
    {
        for (int i = 0; i < actionButtonsHolder.transform.childCount; i++)
        {
            if (actionButtonsHolder.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                Button button = actionButtonsHolder.transform.GetChild(i).gameObject.GetComponent<Button>();
                if (button)
                {
                    button.interactable = !button.interactable;
                }
            }
        }
    }

    /// <summary>
    /// Fills the capacity area with a list of the ally's monster capacities
    /// </summary>
    /// <param name="allyMonsterCapacities">List of all the ally's capacities</param>
    public void FillCapacityArea(List<CapacityScriptableObject> allyMonsterCapacities)
    {
        if (capacitiesContainer.transform.childCount > 0)
        {
            for (int i = 0; i < capacitiesContainer.transform.childCount; i++)
            {
                Destroy(capacitiesContainer.transform.GetChild(i).gameObject);
            }
        }
        foreach (var capacity in allyMonsterCapacities)
        {
            GameObject prefab = Instantiate(capacityItemPrefab, capacitiesContainer.transform);
            if (prefab.GetComponent<CapacityItemHandler>())
            {
                CapacityItemHandler handler = prefab.GetComponent<CapacityItemHandler>();
                handler.capacityName.text = capacity.capacityName;
                handler.capacityPowerValue.text = capacity.strength.ToString();
                handler.capacitySPValue.text = capacity.spValue.ToString();
                handler.capacityTypeValue.text = capacity.type.typeName;
                handler.capacityDetails.text = capacity.description;
                handler.useCapacityButton.onClick.AddListener(() => UseMonsterCapacity(capacity));
                if (capacity.spValue > GameManager.Instance.battleManager.allyMonster.spiritPower)
                {
                    handler.useCapacityButton.interactable = false;
                }
            }
        }
    }

    /// <summary>
    /// Method that is called when entering a battle. Fills all the informations in the battle UI
    /// </summary>
    /// <param name="ally">MonsterScriptableObject of the Ally</param>
    /// <param name="enemy">MonsterScriptableObject of the Enemy</param>
    public void InitializeUIBattle(MonsterScriptableObject ally, MonsterScriptableObject enemy)
    {
        // Ally initialization
        UpdateAllyHP(ally.health, ally.maxHealth);
        UpdateAllySP(ally.spiritPower, ally.maxSpiritPower);
        UpdateAllyXP(ally.currentXp, ally.xpToLevelUp);
        UpdateAllyName(ally.monsterName);
        UpdateAllySprite(ally.backSprite);
        UpdateAllyLevel(ally.level);

        // Enemy initialization
        UpdateEnemyHP(enemy.health, enemy.maxHealth);
        UpdateEnemySP(enemy.spiritPower, enemy.maxSpiritPower);
        UpdateEnemySprite(enemy.frontSprite);
        UpdateEnemyName(enemy.monsterName);
        UpdateEnemyLevel(enemy.level);
    }

    /// <summary>
    /// Called when the user press "use" on a capacity. Provides informations to the Battle Manager
    /// </summary>
    /// <param name="capacity">Used capacity</param>
    public void UseMonsterCapacity(CapacityScriptableObject capacity)
    {
        HandleActiveActionsButtons();
        // HandleInteractableButtons(); A REACTIVER SI ANIMATIONS DE FIGHT GEREES PAS AVANT SINON CA FAIT AUCUN SENS
        capacityArea.SetActive(false);
        bm.allyChoice = BattleManager.BattleChoice.Capacity;
        bm.allyCapacity = capacity;
        bm.hasAllyPlayed = true;
        actualUIDisplayed = UITypes.Classic;
    }

    // Method that handles the end of a battle for the UI
    public void ExitBattle()
    {
        bm.gameObject.SetActive(false);
    }

    public void EnemySwitch(MonsterScriptableObject enemy)
    {
        UpdateEnemyHP(enemy.health, enemy.maxHealth);
        UpdateEnemySP(enemy.spiritPower, enemy.maxSpiritPower);
        UpdateEnemySprite(enemy.frontSprite);
        UpdateEnemyName(enemy.monsterName);
        UpdateEnemyLevel(enemy.level);
    }

    public void AllySwitch(MonsterScriptableObject ally)
    {
        UpdateAllyHP(ally.health, ally.maxHealth);
        UpdateAllySP(ally.spiritPower, ally.maxSpiritPower);
        UpdateAllyXP(ally.currentXp, ally.xpToLevelUp);
        UpdateAllyName(ally.monsterName);
        UpdateAllySprite(ally.backSprite);
        UpdateAllyLevel(ally.level);
        FillCapacityArea(ally.capacitiesList);
    }
}
