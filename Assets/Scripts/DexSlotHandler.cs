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
            // Full infos
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

    public void OnPointerClick(PointerEventData eventData)
    {
        MonsterReportUI.onClickMonsterDetails.Invoke(monster);
    }
}
