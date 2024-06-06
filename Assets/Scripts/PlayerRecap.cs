using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerRecap : MonoBehaviour
{
    public static UnityEvent onPlayerRecapRequested = new UnityEvent();
    public Sprite playerSprite;
    public string playerName;
    public int money;
    public float playTime;
    private BadgeScriptableObject currentBadge;
    public List<BadgeScriptableObject> badges;
    private int badgeLvl;
    [SerializeField] private PlayerRecapUI recapUI;

    private void Awake()
    {
        badgeLvl = 0;
        currentBadge = badges[badgeLvl];
        onPlayerRecapRequested.AddListener(InitializeRecapUI);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playTime += Time.deltaTime;
    }

    private void InitializeRecapUI()
    {
        recapUI.playerFullSprite.sprite = playerSprite;
        recapUI.playerName.text = playerName;
        recapUI.money.text = $"{money.ToString()} G.";
        float hours = Mathf.RoundToInt(playTime / 3600);
        float minutes = Mathf.RoundToInt((playTime % 3600) / 60);
        recapUI.playTime.text = $"{hours.ToString()}h {minutes.ToString()}min";
        recapUI.badgeTitle.text = currentBadge.badgeName;
        recapUI.badgeDescription.text = currentBadge.badgeDescription;
        recapUI.investigatorBadgeSprite.sprite = currentBadge.badgeSprite;
        recapUI.investigatorBadgeSprite.preserveAspect = true;
    }
}

public class InvestigatorLvlUpEvent : UnityEvent<BadgeScriptableObject>
{

}