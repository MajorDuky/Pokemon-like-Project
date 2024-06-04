using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerRecapUI : MonoBehaviour
{
    [SerializeField] private Image playerFullSprite;
    [SerializeField] private Image investigatorBadgeSprite;
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private TMP_Text money;
    [SerializeField] private TMP_Text playTime;
    [SerializeField] private TMP_Text badgeTitle;
    [SerializeField] private TMP_Text badgeDescription;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClosePlayerRecap()
    {
        gameObject.SetActive(false);
    }
}
