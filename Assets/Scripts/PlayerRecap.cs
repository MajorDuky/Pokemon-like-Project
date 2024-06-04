using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecap : MonoBehaviour
{
    public string playerName;
    public int money;
    public float playTime;
    public BadgeLevel currentBadge;

    public enum BadgeLevel
    {
        Junior = 0,
        Medium = 1,
        Advanced = 2
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
}
