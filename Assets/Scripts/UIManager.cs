using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private void OnEnable()
    {
        ItemDisplay.onUse.AddListener(ResponseToItemUsed);
    }
    
    private void ResponseToItemUsed(PickupableItemScriptableObject item)
    {
        HealItemScriptableObject healItem = item as HealItemScriptableObject;
        if (healItem)
        {
            GameManager.Instance.teamUIHandler.gameObject.SetActive(true);
            GameManager.Instance.teamUIHandler.HealUIInitialization(healItem);
        }
    }
}
