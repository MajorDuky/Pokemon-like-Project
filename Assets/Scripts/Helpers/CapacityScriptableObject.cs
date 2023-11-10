using UnityEngine;

[CreateAssetMenu(fileName = "CapacityData", menuName = "ScriptableObjects/CapacityScriptableObject")]
public class CapacityScriptableObject : ScriptableObject
{
    public string capacityName;
    public float strenght;
    public float accuracy;
    public TypeScriptableObject type;
    public string description;
}