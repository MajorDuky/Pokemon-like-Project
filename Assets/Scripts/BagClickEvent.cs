using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BagClickEvent : MonoBehaviour, IPointerClickHandler
{
    public BagUI bagUI;

    public void OnPointerClick(PointerEventData eventData)
    {
        bagUI.gameObject.SetActive(true);
    }
}

