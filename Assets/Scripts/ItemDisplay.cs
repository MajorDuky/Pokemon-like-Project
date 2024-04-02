using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDisplay : MonoBehaviour
{
    public TMP_Text itemName;
    public TMP_Text itemDesc;
    public TMP_Text quantity;
    public Image imageComponent;

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
        quantity.text = "1"; // PENSE A GERER LES QUANTITES TROUDUC
    }
}
