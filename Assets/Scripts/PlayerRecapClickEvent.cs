using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerRecapClickEvent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PlayerRecapUI playerRecapUI;
    public void OnPointerClick(PointerEventData eventData)
    {
        playerRecapUI.gameObject.SetActive(true);
    }
}
