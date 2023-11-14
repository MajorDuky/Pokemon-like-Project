using UnityEngine;

[CreateAssetMenu(fileName = "CapacityData", menuName = "ScriptableObjects/CapacityScriptableObject")]
public class CapacityScriptableObject : ScriptableObject
{
    public string capacityName;
    public float strength;
    public float accuracy;
    public TypeScriptableObject type;
    public string description;
    public float spValue;
}