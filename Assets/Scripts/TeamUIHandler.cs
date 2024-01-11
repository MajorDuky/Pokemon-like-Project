using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class TeamUIHandler : MonoBehaviour
{
    [SerializeField] private Transform monsterContainer;
    [SerializeField] private Transform capacityContainer;
    [SerializeField] private MonsterDisplayHandler monsterDisplayPrefab;
    [SerializeField] private CapacityDisplayHandler capacityDisplayPrefab;
    [SerializeField] private Transform capacitiesDisplayer;
    [SerializeField] private BattleManager bm;

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
        }
    }

    void OnDisable()
    {
        for (int i = 0; i < monsterContainer.childCount; i++)
        {
            Destroy(monsterContainer.GetChild(i).gameObject);
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
