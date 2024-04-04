using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BagUI : MonoBehaviour
{
    public static TabSwitchEvent onTabSwitch = new TabSwitchEvent();
    [SerializeField] private RectTransform itemContainer;
    public enum Tabs
    {
        KeyItems = 0,
        HealItems = 1,
        OtherItems = 2
    }

    private void OnEnable()
    {
        onTabSwitch.Invoke(Tabs.HealItems, itemContainer);
    }

    public void CloseBag()
    {
        gameObject.SetActive(false);
    }

    public void DisplayTab(Tabs tabToDisplay)
    {

    }
}

[System.Serializable]
public class TabSwitchEvent : UnityEvent<BagUI.Tabs, RectTransform>
{

}
