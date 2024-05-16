using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MonsterReportDetailsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text monsterName;
    [SerializeField] private TMP_Text area;
    [SerializeField] private TMP_Text description;
    [SerializeField] private Image sprite;
    [SerializeField] private RectTransform typesSpriteContainer;
    [SerializeField] private RectTransform weakTypesSpritesContainer;
    [SerializeField] private RectTransform strongTypesSpritesContainer;
    [SerializeField] private RectTransform ignoreTypesSpritesContainer;

    private void OnEnable()
    {
        if(typesSpriteContainer.childCount > 0)
        {
            EmptyIconContainer(typesSpriteContainer);
        }

        if(weakTypesSpritesContainer.childCount > 0)
        {
            EmptyIconContainer(weakTypesSpritesContainer);
        }

        if(strongTypesSpritesContainer.childCount > 0)
        {
            EmptyIconContainer(strongTypesSpritesContainer);
        }

        if(ignoreTypesSpritesContainer.childCount > 0)
        {
            EmptyIconContainer(ignoreTypesSpritesContainer);
        }
    }

    public void FillDetails(MonsterScriptableObject monster)
    {
        monsterName.text = monster.monsterName;
        area.text = monster.spawnAreaName;
        description.text = monster.monsterDescription;
        sprite.sprite = monster.frontSprite;
        foreach (TypeScriptableObject type in monster.typesList)
        {
            GameObject newType = GenerateNewTypeIconObject(type);
            newType.transform.SetParent(typesSpriteContainer.transform);
        }
        foreach (TypeScriptableObject type in monster.strengthsList)
        {
            GameObject newType = GenerateNewTypeIconObject(type);
            newType.transform.SetParent(strongTypesSpritesContainer.transform);
        }
        foreach (TypeScriptableObject type in monster.weaknessesList)
        {
            GameObject newType = GenerateNewTypeIconObject(type);
            newType.transform.SetParent(weakTypesSpritesContainer.transform);
        }
        foreach (TypeScriptableObject type in monster.ignoreDmgList)
        {
            GameObject newType = GenerateNewTypeIconObject(type);
            newType.transform.SetParent(ignoreTypesSpritesContainer.transform);
        }
    }

    private GameObject GenerateNewTypeIconObject(TypeScriptableObject type)
    {
        GameObject newType = new GameObject();
        Image newTypeSprite = newType.AddComponent<Image>();
        newTypeSprite.sprite = type.typeSprite;
        return newType;
    }

    private void EmptyIconContainer(RectTransform containerToEmpty)
    {
        for (int containerChild = 0; containerChild < containerToEmpty.childCount; containerChild++)
        {
            Destroy(containerToEmpty.GetChild(containerChild).gameObject);
        }
    }

    public void CloseDetails()
    {
        gameObject.SetActive(false);
    }
}
