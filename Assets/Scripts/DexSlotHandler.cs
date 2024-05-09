using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DexSlotHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text monsterName;
    [SerializeField] private Image monsterSubmitStatusSprite;
    public MonsterScriptableObject monster;

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
        MonsterReportUI.onClickMonsterDetails.Invoke(monster); // Réfléchir à comment transmettre état de découverte du monster
    }
}