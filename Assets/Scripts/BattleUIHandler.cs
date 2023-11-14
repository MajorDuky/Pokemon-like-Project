using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] private Slider allyXPSlider;
    [SerializeField] private Image allySprite;
    [SerializeField] private Image enemySprite;
    [SerializeField] private TMP_Text battleText;
    [SerializeField] private GameObject actionButtonsHolder;
    [SerializeField] private GameObject capacityArea;
    [SerializeField] private GameObject capacitiesContainer;
    [SerializeField] private GameObject capacityItemPrefab;
    private enum UITypes
    {
        Classic = 0,
        Capacity = 1,
        Team = 2,
        Attack = 3,
        Run = 4
    }
    private UITypes actualUIDisplayed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        actualUIDisplayed = UITypes.Classic;
        battleText.text = "Choose your next move.";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAllyName(string name)
    {
        allyNameText.text = name;
    }

    public void UpdateEnemyName(string name)
    {
        enemyNameText.text = name;
    }

    public void UpdateAllyHP(float newAmount, float maxHP)
    {
        allyHPText.text = $"{newAmount.ToString()} / {maxHP.ToString()}";
        allyHPSlider.value = maxHP / newAmount;
    }

    public void UpdateEnemyHP(float newAmount, float maxHP)
    {
        enemyHPText.text = $"{newAmount.ToString()} / {maxHP.ToString()}";
        enemyHPSlider.value = maxHP / newAmount;
    }

    public void UpdateAllySP(float newAmount, float maxSP)
    {
        allySPText.text = $"{newAmount.ToString()} / {maxSP.ToString()}";
        allySPSlider.value = maxSP / newAmount;
    }

    public void UpdateEnemySP(float newAmount, float maxSP)
    {
        enemySPText.text = $"{newAmount.ToString()} / {maxSP.ToString()}";
        enemySPSlider.value = maxSP / newAmount;
    }

    public void UpdateAllyXP(float newAmount, float maxXP)
    {
        allyXPSlider.value = maxXP / newAmount;
    }

    public void UpdateAllySprite(Sprite sprite)
    {
        allySprite.sprite = sprite;
    }

    public void UpdateEnemySprite(Sprite sprite)
    {
        enemySprite.sprite = sprite;
    }

    public void UseCapacityButton()
    {
        actualUIDisplayed = UITypes.Capacity;
        battleText.text = "Choose the capacity you want to use.";
        HandleActiveActionsButtons();
        capacityArea.SetActive(true);
    }

    public void UseAttackButton()
    {
        actualUIDisplayed = UITypes.Attack;
        battleText.text = "Do you want to use a basic attack ?";
        HandleActiveActionsButtons();
    }

    public void UseTeamButton()
    {
        actualUIDisplayed = UITypes.Team;
        HandleActiveActionsButtons();
    }

    public void UseRunButton()
    {
        actualUIDisplayed = UITypes.Run;
        battleText.text = "Do you really want to run away ?";
        HandleActiveActionsButtons();
    }

    public void UseReturnButton()
    {
        switch (actualUIDisplayed)
        {
            case UITypes.Capacity:
                capacityArea.SetActive(false);
                battleText.text = "Choose your next move.";
                HandleActiveActionsButtons();
                break;
            case UITypes.Team:
                battleText.text = "Choose your next move.";
                HandleActiveActionsButtons();
                break;
            case UITypes.Attack:
                battleText.text = "Choose your next move.";
                HandleActiveActionsButtons();
                break;
            case UITypes.Run:
                battleText.text = "Choose your next move.";
                HandleActiveActionsButtons();
                break;
            default:
                break;
        }
    }

    public void UseConfirmButton()
    {
        switch (actualUIDisplayed)
        {
            case UITypes.Attack:
                HandleActiveActionsButtons();
                // Launch Attack
                break;
            case UITypes.Run:
                HandleActiveActionsButtons();
                // Launch Run Away Sequence
                break;
        }
    }

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

    public void FillCapacityArea(MonsterScriptableObject allyMonster)
    {
        foreach (var capacity in allyMonster.capacitiesList)
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
            }
        }
    }

    public void InitializeUIBattle(MonsterScriptableObject ally, MonsterScriptableObject enemy)
    {
        // Ally initialization
        UpdateAllyHP(ally.health, ally.maxHealth);
        UpdateAllySP(ally.spiritPower, ally.maxSpiritPower);
        UpdateAllyXP(ally.currentXp, ally.xpToLevelUp);
        UpdateAllyName(ally.monsterName);
        UpdateAllySprite(ally.backSprite);
        FillCapacityArea(ally);

        // Enemy initialization
        UpdateEnemyHP(enemy.health, enemy.maxHealth);
        UpdateEnemySP(enemy.spiritPower, enemy.maxSpiritPower);
        UpdateEnemySprite(enemy.frontSprite);
        UpdateEnemyName(enemy.monsterName);
    }
}
