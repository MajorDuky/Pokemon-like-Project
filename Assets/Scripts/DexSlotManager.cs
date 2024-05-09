using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DexSlotManager : MonoBehaviour
{
    public List<MonsterScriptableObject> fullDexList;
    [SerializeField] private DexSlotHandler dexSlot;

    private void Awake()
    {
        MonsterReportUI.onInitialization.AddListener(InitializeDexList);
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
        }
    }
}
