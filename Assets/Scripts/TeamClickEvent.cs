using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TeamClickEvent : MonoBehaviour, IPointerClickHandler
{
    public TeamUIHandler teamUIHandler;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        teamUIHandler.gameObject.SetActive(true);
    }
}
