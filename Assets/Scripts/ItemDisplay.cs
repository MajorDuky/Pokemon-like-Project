using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ItemDisplay : MonoBehaviour
{
    public TMP_Text itemName;
    public TMP_Text itemDesc;
    public TMP_Text quantity;
    public Image imageComponent;
    public PickupableItemScriptableObject itemData;
    public static UseItemEvent onUse = new UseItemEvent();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FillInformations(PickupableItemScriptableObject item)
    {
        itemName.text = item.itemName;
        itemDesc.text = item.itemDescription;
        imageComponent.sprite = item.itemSprite;
        quantity.text = item.itemQuantity.ToString();
        itemData = item;
    }

    public void UseItem()
    {
        onUse.Invoke(itemData);
    }
}

public class UseItemEvent : UnityEvent<PickupableItemScriptableObject>
{

}
