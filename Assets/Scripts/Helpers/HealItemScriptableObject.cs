using UnityEngine;

[CreateAssetMenu(fileName = "HealItemData", menuName = "ScriptableObjects/HealItemScriptableObject")]
public class HealItemScriptableObject : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    public float healAmount;
    public bool isUsable;
}