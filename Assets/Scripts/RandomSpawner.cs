using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public List<MonsterScriptableObject> encounterableMonsters;
    public string areaName;
    [SerializeField] private int areaLvl;
    [SerializeField] private float probabilityOfEncounter;
    [SerializeField] private float probabilityCalcRate;
    [SerializeField] private DangerGauge dangerGauge;
    public bool isInArea = false;
    public bool isCoroutineRunning = false;
    public float baseXPGain;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!isInArea)
        {
            GameManager.Instance.currentSpawner = this;
            GameManager.Instance.baseXPGain = baseXPGain;
        }
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
        GameManager.Instance.currentSpawner = null;
        StopCoroutine(FightProbabiltyCoroutine());
        dangerGauge.StopDangerGauge();
        GameManager.Instance.baseXPGain = 0f;
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

                    if(!randomPickedMonster.hasBeenEncountered)
                    {
                        randomPickedMonster.RegisterMonster(areaName);
                    }
                    
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
