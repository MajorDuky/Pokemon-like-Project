using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagHandler : MonoBehaviour
{
    public List<PickupableItemScriptableObject> healItemList = new List<PickupableItemScriptableObject>();
    [SerializeField] private ItemDisplay itemDisplayPrefab;
    [SerializeField] private ItemDisplay usableItemDisplayPrefab;

    private void Awake()
    {
        PickupableItem.onPickup.AddListener(AddItemToBag);
        BagUI.onTabSwitch.AddListener(FillBagTab);
    }

    private void AddItemToBag(PickupableItemScriptableObject item)
    {
        HealItemScriptableObject healItem = item as HealItemScriptableObject;
        if(healItem)
        {
            healItem.itemQuantity++;
            if (!healItem.isInBag)
            {
                healItem.isInBag = true;
                healItemList.Add(healItem);
            }
        }
        // AUTRES TYPES D'ITEMS
    }

    private void FillBagTab(BagUI.Tabs tabsToDisplay, RectTransform itemContainer)
    {
        
        if (itemContainer.childCount > 0)
        {
            for (int child = 0; child < itemContainer.childCount; child++)
            {
                Destroy(itemContainer.GetChild(child).gameObject);
            }
        }
        
        switch (tabsToDisplay)
        {
            case BagUI.Tabs.KeyItems:
                break;
            case BagUI.Tabs.HealItems:
                GenerateItemList(healItemList, itemContainer);
                break;
            case BagUI.Tabs.OtherItems:
                break;
            default:
                break;
        }
    }

    private void GenerateItemList(List<PickupableItemScriptableObject> itemList, Transform itemContainer)
    {
        foreach (var item in itemList)
        {
            if (item.isUsable)
            {
                ItemDisplay clone = Instantiate(usableItemDisplayPrefab, itemContainer);
                clone.FillInformations(item);
            }
            else
            {
                ItemDisplay clone = Instantiate(itemDisplayPrefab, itemContainer);
                clone.FillInformations(item);
            }
        }
    }
}
