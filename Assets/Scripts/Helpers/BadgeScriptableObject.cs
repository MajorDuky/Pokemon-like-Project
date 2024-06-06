using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BadgeData", menuName = "ScriptableObjects/BadgeScriptableObject")]
public class BadgeScriptableObject : ScriptableObject
{
    public string badgeName;
    public string badgeDescription;
    public Sprite badgeSprite;
}
