using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class MonsterDisplayHandler : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private TMP_Text monsterName;
    [SerializeField] private TMP_Text monsterLvl;
    [SerializeField] private TMP_Text monsterTypes;
    [SerializeField] private TMP_Text monsterHealth;
    [SerializeField] private TMP_Text monsterSP;
    [SerializeField] private TMP_Text monsterXP;
    [SerializeField] private Slider sliderHealth;
    [SerializeField] private Slider sliderSP;
    [SerializeField] private Slider sliderXP;
    [SerializeField] private Image sprite;
    public Button showCapacitiesBtn;
    public Button switchMonsterButton;
    [SerializeField] private RectTransform selectedSprite;
    [SerializeField] private Selectable selectable;

    public void UpdateName(string name)
    {
        monsterName.text = name;
    }

    public void UpdateLevel(int lvl)
    {
        monsterLvl.text = lvl.ToString();
    }

    public void UpdateTypes(List<TypeScriptableObject> types)
    {
        if (types.Count == 1)
        {
            monsterTypes.text = types[0].typeName;
        }
        else
        {
            for (int i = 0; i < types.Count; i++)
            {
                if (i == types.Count - 1)
                {
                    monsterTypes.text += types[i].typeName;
                }
                else
                {
                    monsterTypes.text += types[i].typeName + " / ";
                }
            }
        }
    }

    public void UpdateHealth(float hp, float maxHp)
    {
        monsterHealth.text = $"{Math.Floor(hp).ToString()} / {Math.Floor(maxHp).ToString()}";
        sliderHealth.value = hp / maxHp;
    }

    public void UpdateSP(float sp, float maxSp)
    {
        monsterSP.text = $"{Math.Floor(sp).ToString()} / {Math.Floor(maxSp).ToString()}";
        sliderSP.value = sp / maxSp;
    }

    public void UpdateXP(float xp, float maxXp)
    {
        monsterXP.text = $"{Math.Floor(xp).ToString()} / {Math.Floor(maxXp).ToString()}";
        sliderXP.value = xp / maxXp;
    }

    public void UpdateSprite(Sprite monsterSprite)
    {
        sprite.sprite = monsterSprite;
    }

    public void OnSelect(BaseEventData baseEvent)
    {
        selectedSprite.gameObject.SetActive(true);
    }
    public void OnDeselect(BaseEventData baseEvent)
    {
        selectedSprite.gameObject.SetActive(false);
    }
}
