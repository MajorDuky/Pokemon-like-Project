using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DexSlotManager : MonoBehaviour
{
    public static MonsterEncounteredEvent onMonsterEncounter = new MonsterEncounteredEvent();
    public List<MonsterScriptableObject> fullDexList;
    [SerializeField] private DexSlotHandler dexSlot;
    private List<DexSlotHandler> slots = new List<DexSlotHandler>();

    private void Awake()
    {
        MonsterReportUI.onInitialization.AddListener(InitializeDexList);
        onMonsterEncounter.AddListener(UpdateSlotDetails);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeDexList(RectTransform container)
    {
        foreach (MonsterScriptableObject monster in fullDexList)
        {
            DexSlotHandler dexSlotClone = Instantiate(dexSlot, container);
            dexSlotClone.monster = monster;
            dexSlotClone.AdjustDisplayDexSlot();
            slots.Add(dexSlotClone);
        }
    }

    private void UpdateSlotDetails(MonsterScriptableObject monster)
    {
        DexSlotHandler slotToUpdate = slots.Find(x => x.monster.monsterName == monster.monsterName);
        slotToUpdate.AdjustDisplayDexSlot();
    }
}

public class MonsterEncounteredEvent : UnityEvent<MonsterScriptableObject>
{
}