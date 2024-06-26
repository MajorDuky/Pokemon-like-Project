using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableItemScriptableObject : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    public int itemQuantity;
    public bool isInBag;
    public bool isUsable;
}
