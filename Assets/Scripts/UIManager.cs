using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private void OnEnable()
    {
        ItemDisplay.healItemUsed.AddListener(ResponseToItemUsed);
    }
    
    private void ResponseToItemUsed(HealItemScriptableObject item)
    {
        GameManager.Instance.teamUIHandler.gameObject.SetActive(true);
        GameManager.Instance.teamUIHandler.HealUIInitialization(item);
    }
}
