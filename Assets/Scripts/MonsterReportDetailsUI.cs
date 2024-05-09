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


    public void FillDetails(MonsterScriptableObject monster)
    {
        monsterName.text = monster.monsterName;
        area.text = monster.spawnAreaName;
        description.text = monster.monsterDescription;
        sprite.sprite = monster.frontSprite;
        foreach (TypeScriptableObject type in monster.typesList)
        {
            GameObject newType = GenerateNewTypeIconObject(type);
            newType.transform.parent = typesSpriteContainer.transform;
        }
        foreach (TypeScriptableObject type in monster.strengthsList)
        {
            GameObject newType = GenerateNewTypeIconObject(type);
            newType.transform.parent = strongTypesSpritesContainer.transform;
        }
        foreach (TypeScriptableObject type in monster.weaknessesList)
        {
            GameObject newType = GenerateNewTypeIconObject(type);
            newType.transform.parent = weakTypesSpritesContainer.transform;
        }
        foreach (TypeScriptableObject type in monster.ignoreDmgList)
        {
            GameObject newType = GenerateNewTypeIconObject(type);
            newType.transform.parent = ignoreTypesSpritesContainer.transform;
        }
    }

    private GameObject GenerateNewTypeIconObject(TypeScriptableObject type)
    {
        GameObject newType = new GameObject();
        Image newTypeSprite = newType.AddComponent<Image>();
        newTypeSprite.sprite = type.typeSprite;
        return newType;
    }

    public void CloseDetails()
    {
        gameObject.SetActive(false);
    }
}
