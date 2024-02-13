using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private List<MonsterScriptableObject> encounterableMonsters;
    [SerializeField] private int areaLvl;
    [SerializeField] private float probabilityOfEncounter;
    [SerializeField] private float probabilityCalcRate;
    private bool isInArea;
    private bool isCoroutineRunning = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        isInArea = true;
        if (!isCoroutineRunning && !GameManager.Instance.isInBattle)
        {
            StartCoroutine(FightProbabiltyCoroutine());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInArea = false;
        StopCoroutine(FightProbabiltyCoroutine());
    }

    IEnumerator FightProbabiltyCoroutine()
    {
        isCoroutineRunning = true;
        while (isInArea)
        {
            yield return new WaitForSeconds(probabilityCalcRate);
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
                GameManager.Instance.StartBattle(monsterToFight, true);
                monsterToFight.Clear();
                isCoroutineRunning = false;
                break;
            }
        }
    }
}
