using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TeamClickEvent : MonoBehaviour, IPointerClickHandler
{
    public TeamUIHandler teamUIHandler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        teamUIHandler.gameObject.SetActive(true);
    }
}
