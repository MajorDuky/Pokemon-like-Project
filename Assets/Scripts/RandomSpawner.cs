using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private List<MonsterScriptableObject> encounterableMonsters;
    [SerializeField] private string areaName;
    [SerializeField] private int areaLvl;
    [SerializeField] private float probabilityOfEncounter;
    [SerializeField] private float probabilityCalcRate;
    [SerializeField] private DangerGauge dangerGauge;
    public bool isInArea = false;
    public bool isCoroutineRunning = false;


    private void OnTriggerStay2D(Collider2D collision)
    {
        isInArea = true;
        if (!isCoroutineRunning && !GameManager.Instance.isInBattle)
        {
            StartCoroutine(FightProbabiltyCoroutine());
        }
        if (!dangerGauge.gameObject.activeInHierarchy && !GameManager.Instance.isInBattle)
        {
            dangerGauge.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInArea = false;
        StopCoroutine(FightProbabiltyCoroutine());
        dangerGauge.StopDangerGauge();
    }

    IEnumerator FightProbabiltyCoroutine()
    {
        isCoroutineRunning = true;
        dangerGauge.gameObject.SetActive(true);
        dangerGauge.InitializeDangerGauge(areaName, areaLvl, probabilityCalcRate);
        while (isInArea)
        {
            yield return new WaitForSeconds(probabilityCalcRate);
            if (isInArea)
            {
                bool startRandomBattle = Random.Range(0f, 100f) <= probabilityOfEncounter;
                if (startRandomBattle)
                {
                    MonsterScriptableObject randomPickedMonster = encounterableMonsters[Random.Range(0, encounterableMonsters.Count)];
                    if (randomPickedMonster.level < areaLvl)
                    {
                        int lvlToGain = areaLvl - randomPickedMonster.level;
                        randomPickedMonster.LevelUp(lvlToGain);
                    }
                    List<MonsterScriptableObject> monsterToFight = new List<MonsterScriptableObject>();
                    monsterToFight.Add(randomPickedMonster);
                    dangerGauge.gameObject.SetActive(false);
                    GameManager.Instance.StartBattle(monsterToFight, true);
                    monsterToFight.Clear();
                    isCoroutineRunning = false;
                    break;
                }
            }
            else
            {
                dangerGauge.gameObject.SetActive(false);
                isCoroutineRunning = false;
                break;
            }
        }
    }
}