using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterReportUI : MonoBehaviour
{
    public static InitializeDexEvent onInitialization = new InitializeDexEvent();
    public static InitializeMonsterReportDetailsEvent onClickMonsterDetails = new InitializeMonsterReportDetailsEvent();
    [SerializeField] private RectTransform content;
    [SerializeField] private MonsterReportDetailsUI monsterReportDetailsUI;

    private void Awake()
    {
        onInitialization.Invoke(content);
    }

    private void OnEnable()
    {
        onClickMonsterDetails.AddListener(DisplayMonsterDetails);
    }
    private void OnDisable()
    {
        onClickMonsterDetails.RemoveListener(DisplayMonsterDetails);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DisplayMonsterDetails(MonsterScriptableObject monster)
    {
        monsterReportDetailsUI.gameObject.SetActive(true);
        monsterReportDetailsUI.FillDetails(monster);
    }
}

public class InitializeDexEvent : UnityEvent<RectTransform>
{

}

public class InitializeMonsterReportDetailsEvent : UnityEvent<MonsterScriptableObject>
{

}
