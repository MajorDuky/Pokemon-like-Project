using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DexSlotHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text monsterName;
    [SerializeField] private Image monsterSubmitStatusSprite;
    public MonsterScriptableObject monster;
    private bool isHoverAnimationOn;
    private Vector3 originalScale;
    private bool isInflating;
    public Vector3 maxInflatedScaleVector;
    public Vector3 inflatingRateVector;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    private void OnEnable()
    {
        isInflating = true;
    }

    public void AdjustDisplayDexSlot()
    {
        if (monster.hasBeenEncountered && monster.isAlly
            || monster.hasBeenEncountered && monster.isNPCMonster)
        {
            RevealFullInformationsOnMonster();
        }
        else if (monster.hasBeenEncountered)
        {
            RevealPartialInformationsOnMonster();
        }
        else
        {
            RevealNoInformationsOnMonster();
        }
    }

    public void RevealFullInformationsOnMonster()
    {
        monsterName.text = monster.monsterName;
        if(monster.isAlly)
        {
            // monsterSubmitStatusSprite.sprite = Sprite soumission
        }
        else
        {
            // monsterSubmitStatusSprite.sprite = Sprite monstre incapturable
        }
    }

    public void RevealPartialInformationsOnMonster()
    {
        monsterName.text = monster.monsterName;
        // monsterSubmitStatusSprite.sprite = Sprite monstre rencontré (oeil ouvert)
    }

    public void RevealNoInformationsOnMonster()
    {
        monsterName.text = "-----------";
        // monsterSubmitStatusSprite.sprite = Sprite monstre non rencontré (oeil fermé)
    }

    public void OnPointerClick(PointerEventData eventData)
    {   
        if(monster.hasBeenEncountered)
        {
            MonsterReportUI.onClickMonsterDetails.Invoke(monster); // Réfléchir à comment transmettre état de découverte du monster
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHoverAnimationOn = true;
        MonsterReportUI.onHoverSlot.Invoke(monster.frontSprite, monster.hasBeenEncountered) ;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHoverAnimationOn = false;
        transform.localScale = originalScale;
    }
    private void Update()
    {
        if(isHoverAnimationOn)
        {
            if(isInflating)
            {
                transform.localScale += inflatingRateVector;
            }
            else
            {
                transform.localScale -= inflatingRateVector;
            }

            if (transform.localScale.x >= maxInflatedScaleVector.x)
            {
                isInflating = false;
            }
            else if (transform.localScale.x <= originalScale.x)
            {
                isInflating = true;
            }
        }
    }
}