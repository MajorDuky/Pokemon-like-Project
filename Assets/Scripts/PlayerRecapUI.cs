using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerRecapUI : MonoBehaviour
{
    public Image playerFullSprite;
    public Image investigatorBadgeSprite;
    public TMP_Text playerName;
    public TMP_Text money;
    public TMP_Text playTime;
    public TMP_Text badgeTitle;
    public TMP_Text badgeDescription;

    private void OnEnable()
    {
        PlayerRecap.onPlayerRecapRequested.Invoke();
    }

    public void ClosePlayerRecap()
    {
        gameObject.SetActive(false);
    }
}
