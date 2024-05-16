using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MonsterReportClickEvent : MonoBehaviour, IPointerClickHandler
{
    public MonsterReportUI monsterReportUI;
    public void OnPointerClick(PointerEventData eventData)
    {
        monsterReportUI.gameObject.SetActive(true);
    }
}
