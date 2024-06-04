using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MonsterReportUI : MonoBehaviour
{
    public static InitializeDexEvent onInitialization = new InitializeDexEvent();
    public static InitializeMonsterReportDetailsEvent onClickMonsterDetails = new InitializeMonsterReportDetailsEvent();
    public static PointerHoversSlotEvent onHoverSlot = new PointerHoversSlotEvent();
    [SerializeField] private RectTransform content;
    [SerializeField] private MonsterReportDetailsUI monsterReportDetailsUI;
    [SerializeField] private Image bigDisplay;
    [SerializeField] Sprite notEncounteredMonsterSprite;
    private bool isInitialized;

    private void OnEnable()
    {
        if(!isInitialized)
        {
            isInitialized = true;
            onInitialization.Invoke(content);
        }
        onClickMonsterDetails.AddListener(DisplayMonsterDetails);
        onHoverSlot.AddListener(SwapMonsterSpriteDisplay);
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

    public void CloseMonsterReport()
    {
        gameObject.SetActive(false);
    }

    private void SwapMonsterSpriteDisplay(Sprite sprite, bool hasMonsterBeenEncountered)
    {
        bigDisplay.sprite = hasMonsterBeenEncountered ? sprite : notEncounteredMonsterSprite;
    }
}

public class InitializeDexEvent : UnityEvent<RectTransform>
{

}

public class InitializeMonsterReportDetailsEvent : UnityEvent<MonsterScriptableObject>
{

}

public class PointerHoversSlotEvent : UnityEvent<Sprite,bool>
{

}
