using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickupableItem : MonoBehaviour
{
    public PickupableItemScriptableObject pickupableItemData;
    private SpriteRenderer spriteRenderer;

    public static PickupEvent onPickup;

    // Start is called before the first frame update
    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = pickupableItemData.itemSprite;
        onPickup = new PickupEvent();
    }
}

[System.Serializable]
public class PickupEvent : UnityEvent<PickupableItemScriptableObject>
{

}


