using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NecronomiconHandler : MonoBehaviour
{
    public List<MonsterScriptableObject> monstersInNecro = new List<MonsterScriptableObject>();
    [SerializeField] private MonsterDisplayHandler monsterDisplayHandler;
    [SerializeField] private Transform displayArea;
    public static SubmissionWithFullTeamEvent onSubmission = new SubmissionWithFullTeamEvent();

    private void Awake()
    {
        onSubmission.AddListener(AddMonsterToNecro);
    }

    private void OnEnable()
    {
        ActualizeNecronomicon();
    }

    public void ActualizeNecronomicon()
    {
        for (int i = 0; i < displayArea.childCount; i++)
        {
            Destroy(displayArea.GetChild(i).gameObject);
        }
        foreach (var monster in monstersInNecro)
        {
            MonsterDisplayHandler clone = Instantiate(monsterDisplayHandler, displayArea);
            clone.UpdateAllInformations(monster);
            clone.monster = monster;
            clone.showCapacitiesBtn.onClick.AddListener(() => GameManager.Instance.teamUIHandler.FillCapacityDisplayer(monster.capacitiesList));
        }
    }

    private void AddMonsterToNecro(MonsterScriptableObject monsterToAdd)
    {
        monsterToAdd.isInNecronomicon = true;
        monstersInNecro.Add(monsterToAdd);
    }
}

public class SubmissionWithFullTeamEvent : UnityEvent<MonsterScriptableObject>
{

}
