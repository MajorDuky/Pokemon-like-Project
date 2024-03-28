using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupItems : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PickupableItem"))
        {
            PickupableItem.onPickup.Invoke(collision.GetComponent<PickupableItem>().pickupableItemData);
            Destroy(collision.gameObject);
        }
    }
}
