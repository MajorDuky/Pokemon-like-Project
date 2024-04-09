using UnityEngine;

[CreateAssetMenu(fileName = "HealItemData", menuName = "ScriptableObjects/HealItemScriptableObject")]
public class HealItemScriptableObject : PickupableItemScriptableObject
{
    public float healAmount;
    public int maxNumberOfMonstersToHeal;
}