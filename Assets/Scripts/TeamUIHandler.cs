using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

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
    public List<MonsterScriptableObject> selectedMonsters;

    // Start is called before the first frame update
    void OnEnable()
    {
        foreach (var monster in GameManager.Instance.playerTeam)
        {
            MonsterDisplayHandler clone = Instantiate(monsterDisplayPrefab, monsterContainer);
            clone.UpdateName(monster.monsterName);
            clone.UpdateLevel(monster.level);
            clone.UpdateTypes(monster.typesList);
            clone.UpdateHealth(monster.health, monster.maxHealth);
            clone.UpdateSP(monster.spiritPower, monster.maxSpiritPower);
            clone.UpdateXP(monster.currentXp, monster.xpToLevelUp);
            clone.UpdateSprite(monster.frontSprite);
            clone.showCapacitiesBtn.onClick.AddListener(() => FillCapacityDisplayer(monster.capacitiesList));
            clone.switchMonsterButton.onClick.AddListener(() => SwitchMonster(monster));
            if(!monster.isAlive)
            {
                clone.switchMonsterButton.interactable = false;
                // pense à faire un sprite tête de mort à afficher à côté du nom du monstre KO.
            }
            if(!GameManager.Instance.isInBattle)
            {
                clone.switchMonsterButton.enabled = false;
            }
        }
        if (!GameManager.Instance.isInBattle)
        {
            activateNecroBtn.gameObject.SetActive(true);
        }
    }

    void OnDisable()
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
    }

    void FillCapacityDisplayer(List<CapacityScriptableObject> capacitiesList)
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
}
