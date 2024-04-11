using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text healItemName;
    [SerializeField] private TMP_Text healItemQuantity;
    [SerializeField] private TMP_Text healItemNameBtn;
    public Button useHealBtn;

    private void OnEnable()
    {
        BagHandler.uiModifNeeded.AddListener(ActualizeHealUIAfterItemUsed);
    }

    private void OnDisable()
    {
        BagHandler.uiModifNeeded.RemoveListener(ActualizeHealUIAfterItemUsed);
    }

    public void InitializeHealUI(HealItemScriptableObject healItem)
    {
        healItemName.text = healItem.itemName;
        healItemNameBtn.text = $"USE {healItem.itemName}";
        healItemQuantity.text = $"X{healItem.itemQuantity}";
    }

    public void ActualizeHealUIAfterItemUsed(PickupableItemScriptableObject item)
    {
        Debug.Log("coucou");
        healItemNameBtn.text = $"USE {item.itemName}";
        healItemQuantity.text = $"X{item.itemQuantity}";
    }
}
