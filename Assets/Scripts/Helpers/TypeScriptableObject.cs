using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TypeData", menuName = "ScriptableObjects/TypeScriptableObject")]
public class TypeScriptableObject : ScriptableObject
{
    public string typeName;
    public int idType;
}