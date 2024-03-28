using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagHandler : MonoBehaviour
{
    public List<HealItemScriptableObject> healItemList = new List<HealItemScriptableObject>();

    private void Start()
    {
        PickupableItem.onPickup.AddListener(AddItemToBag);
    }

    private void AddItemToBag(PickupableItemScriptableObject item)
    {
        HealItemScriptableObject healItem = item as HealItemScriptableObject;
        if(healItem)
        {
            healItemList.Add(healItem);
        }
        // AUTRES TYPES D'ITEMS
    }
}
