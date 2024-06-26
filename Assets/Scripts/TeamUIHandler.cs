using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeamUIHandler : MonoBehaviour
{
    [SerializeField] private RectTransform monsterContainer;
    [SerializeField] private RectTransform teamContainer;
    [SerializeField] private Transform capacityContainer;
    [SerializeField] private MonsterDisplayHandler monsterDisplayPrefab;
    [SerializeField] private CapacityDisplayHandler capacityDisplayPrefab;
    [SerializeField] private Transform capacitiesDisplayer;
    [SerializeField] private BattleManager bm;
    [SerializeField] private Button activateNecroBtn;
    [SerializeField] private Button deactivateNecroBtn;
    [SerializeField] private Transform necronomiconUI;
    [SerializeField] private Button retireMonsterBtn;
    [SerializeField] private Button storeMonsterBtn;
    [SerializeField] private Button switchMonsterBtn;
    [HideInInspector] public List<MonsterScriptableObject> selectedMonsters = new List<MonsterScriptableObject>();
    [SerializeField] private Transform tutoSection;
    [SerializeField] private TMP_Text organizeHelperText;
    [SerializeField] private NecronomiconHandler necronomiconHandler;
    [SerializeField] private Transform playerActions;
    [HideInInspector] public bool isHealItemBeingUsed;
    [HideInInspector] public HealItemScriptableObject healItem;
    [SerializeField] private HealUIManager healUiManager;

    // Start is called before the first frame update
    void OnEnable()
    {
        ActualizePlayerTeam();
        if (!GameManager.Instance.isInBattle)
        {
            activateNecroBtn.gameObject.SetActive(true);
            playerActions.gameObject.SetActive(true);
        }
        else
        {
            playerActions.gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        CloseTeamUI();
    }

    public void ActualizePlayerTeam()
    {
        for (int i = 0; i < monsterContainer.childCount; i++)
        {
            Destroy(monsterContainer.GetChild(i).gameObject);
        }
        foreach (var monster in GameManager.Instance.playerTeam)
        {
            MonsterDisplayHandler clone = Instantiate(monsterDisplayPrefab, monsterContainer);
            clone.UpdateAllInformations(monster);
            clone.monster = monster;
            clone.showCapacitiesBtn.onClick.AddListener(() => FillCapacityDisplayer(monster.capacitiesList));
            clone.switchMonsterButton.onClick.AddListener(() => SwitchMonster(monster));
            if (!monster.isAlive)
            {
                clone.switchMonsterButton.interactable = false;
                // pense � faire un sprite t�te de mort � afficher � c�t� du nom du monstre KO.
            }
            if (!GameManager.Instance.isInBattle)
            {
                clone.switchMonsterButton.gameObject.SetActive(false);
            }
        }
    }

    public void FillCapacityDisplayer(List<CapacityScriptableObject> capacitiesList)
    {
        capacitiesDisplayer.gameObject.SetActive(true);
        foreach (var capacity in capacitiesList)
        {
            CapacityDisplayHandler clone = Instantiate(capacityDisplayPrefab, capacityContainer);
            clone.UpdateCapacityName(capacity.capacityName);
            clone.UpdateCapacityDesc(capacity.description);
            clone.UpdateCapacitySp(capacity.spValue);
            clone.UpdateCapacityAccuracy(capacity.accuracy);
            clone.UpdateCapacityStrength(capacity.strength);
            clone.UpdateCapacityType(capacity.type.typeName);
        }
    }

    public void CloseCapacityDisplayer()
    {
        for (int i = 0; i < capacityContainer.childCount; i++)
        {
            Destroy(capacityContainer.GetChild(i).gameObject);
        }
        capacitiesDisplayer.gameObject.SetActive(false);
    }

    public void SwitchMonster(MonsterScriptableObject newMonster)
    {
        bm.newPlayerMonster = newMonster;
        bm.allyChoice = BattleManager.BattleChoice.Switch;
        bm.hasAllyPlayed = true;
        bm.ui.UseConfirmButton();
        bm.ui.UpdateBattleText("You switched your monster for " + newMonster.monsterName + " !");
        gameObject.SetActive(false);
    }

    public void ActivateNecro()
    {
        teamContainer.sizeDelta = new Vector2(400, teamContainer.sizeDelta.y);
        necronomiconUI.gameObject.SetActive(true);
        activateNecroBtn.gameObject.SetActive(false);
        deactivateNecroBtn.gameObject.SetActive(true);
        retireMonsterBtn.gameObject.SetActive(true);
        storeMonsterBtn.gameObject.SetActive(true);
        switchMonsterBtn.gameObject.SetActive(true);
    }

    public void DeactivateNecro()
    {
        teamContainer.sizeDelta = new Vector2(800, teamContainer.sizeDelta.y);
        necronomiconUI.gameObject.SetActive(false);
        deactivateNecroBtn.gameObject.SetActive(false);
        activateNecroBtn.gameObject.SetActive(true);
        retireMonsterBtn.gameObject.SetActive(false);
        storeMonsterBtn.gameObject.SetActive(false);
        switchMonsterBtn.gameObject.SetActive(false);
    }

    public void HandleActiveActionButtons()
    {
        if (!isHealItemBeingUsed)
        {
            storeMonsterBtn.interactable = false;
            retireMonsterBtn.interactable = false;
            switchMonsterBtn.interactable = false;
            tutoSection.gameObject.SetActive(false);

            if (selectedMonsters.Count != 0)
            {
                int monsterSelectedInTeam = 0;
                int monsterSelectedInNecro = 0;
                int deltaMissingMonsters = GameManager.Instance.maxMonsterInTeam - GameManager.Instance.playerTeam.Count;

                foreach (var currentMonster in selectedMonsters)
                {
                    if (currentMonster.isInNecronomicon)
                    {
                        monsterSelectedInNecro++;
                    }
                    else
                    {
                        monsterSelectedInTeam++;
                    }
                }

                if (monsterSelectedInTeam > 0 && monsterSelectedInNecro == 0)
                {
                    if (monsterSelectedInTeam == GameManager.Instance.maxMonsterInTeam - deltaMissingMonsters)
                    {
                        tutoSection.gameObject.SetActive(true);
                        organizeHelperText.text = "You can't have an empty team !";
                    }
                    else
                    {
                        storeMonsterBtn.interactable = true;
                    }
                }
                else if (monsterSelectedInTeam == 0 && monsterSelectedInNecro > 0)
                {
                    if (monsterSelectedInNecro > GameManager.Instance.maxMonsterInTeam)
                    {
                        tutoSection.gameObject.SetActive(true);
                        organizeHelperText.text = "Too many monsters selected in the Necronomicon !";
                    }
                    else if (monsterSelectedInNecro > deltaMissingMonsters)
                    {
                        tutoSection.gameObject.SetActive(true);
                        organizeHelperText.text = "Too few remaining slots in your team !";
                    }
                    else
                    {
                        retireMonsterBtn.interactable = true;
                    }
                }
                else
                {
                    if (monsterSelectedInNecro > deltaMissingMonsters + monsterSelectedInTeam ||
                        monsterSelectedInNecro > GameManager.Instance.maxMonsterInTeam)
                    {
                        tutoSection.gameObject.SetActive(true);
                        organizeHelperText.text = "Too many monsters selected in the Necronomicon !";
                    }
                    else
                    {
                        switchMonsterBtn.interactable = true;
                    }
                }
            }
        }
        else
        {
            int countSelectedMonsters = selectedMonsters.Count;
            if (countSelectedMonsters > healItem.maxNumberOfMonstersToHeal)
            {
                healUiManager.useHealBtn.interactable = false;
                tutoSection.gameObject.SetActive(true);
                organizeHelperText.text = "Too many monsters selected !";
            }
            else if (countSelectedMonsters > 0 && countSelectedMonsters <= healItem.maxNumberOfMonstersToHeal)
            {
                healUiManager.useHealBtn.interactable = true;
                tutoSection.gameObject.SetActive(false);
            }
            else
            {
                healUiManager.useHealBtn.interactable = false;
                tutoSection.gameObject.SetActive(true);
                organizeHelperText.text = $"Choose {healItem.maxNumberOfMonstersToHeal} monster(s) to heal";
            }
        }
    }

    public void RetireMonster()
    {
        foreach (var monster in selectedMonsters)
        {
            monster.isInNecronomicon = false;
            GameManager.Instance.playerTeam.Add(monster);
            necronomiconHandler.monstersInNecro.Remove(monster);
        }
        necronomiconHandler.ActualizeNecronomicon();
        ActualizePlayerTeam();
        selectedMonsters.Clear();
    }

    public void StoreMonster()
    {
        foreach (var monster in selectedMonsters)
        {
            monster.isInNecronomicon = true;
            GameManager.Instance.playerTeam.Remove(monster);
            necronomiconHandler.monstersInNecro.Add(monster);
        }
        necronomiconHandler.ActualizeNecronomicon();
        ActualizePlayerTeam();
        selectedMonsters.Clear();
    }

    public void SwitchMonstersBetweenTeamAndNecro()
    {
        foreach (var monster in selectedMonsters)
        {
            if (monster.isInNecronomicon)
            {
                monster.isInNecronomicon = false;
                GameManager.Instance.playerTeam.Add(monster);
                necronomiconHandler.monstersInNecro.Remove(monster);
            }
            else
            {
                monster.isInNecronomicon = true;
                GameManager.Instance.playerTeam.Remove(monster);
                necronomiconHandler.monstersInNecro.Add(monster);
            }
        }
        necronomiconHandler.ActualizeNecronomicon();
        ActualizePlayerTeam();
        selectedMonsters.Clear();
    }

    public void CloseTeamUI()
    {
        for (int i = 0; i < monsterContainer.childCount; i++)
        {
            Destroy(monsterContainer.GetChild(i).gameObject);
        }
        if (!GameManager.Instance.isInBattle && activateNecroBtn.gameObject.activeInHierarchy)
        {
            activateNecroBtn.gameObject.SetActive(false);
        }
        if (!GameManager.Instance.isInBattle && necronomiconUI.gameObject.activeInHierarchy)
        {
            DeactivateNecro();
        }
        if (healUiManager.gameObject.activeInHierarchy)
        {
            healUiManager.gameObject.SetActive(false);
            isHealItemBeingUsed = false;
        }
        gameObject.SetActive(false);
    }

    public void HealUIInitialization(HealItemScriptableObject item)
    {
        healUiManager.gameObject.SetActive(true);
        healUiManager.InitializeHealUI(item);
        isHealItemBeingUsed = true;
        healItem = item;
        activateNecroBtn.gameObject.SetActive(false);
        healUiManager.useHealBtn.gameObject.SetActive(true);
        healUiManager.useHealBtn.interactable = false;
        tutoSection.gameObject.SetActive(true);
        organizeHelperText.text = $"Choose {healItem.maxNumberOfMonstersToHeal} monster(s) to heal";
    }

    public void UseHealItem()
    {
        foreach (MonsterScriptableObject monster in selectedMonsters)
        {
            monster.Heal(healItem.healAmount);
        }
        ActualizePlayerTeam();
        ItemDisplay.onUse.Invoke(healItem);
        selectedMonsters.Clear();
    }
}
