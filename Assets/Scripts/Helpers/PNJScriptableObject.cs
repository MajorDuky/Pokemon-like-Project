using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PNJData", menuName = "ScriptableObjects/PNJScriptableObject", order = 2)]
public class PNJScriptableObject : ScriptableObject
{
    public Sprite sprite;
    public Vector2 spawnLocation;
    public List<string> dialogue;
    public List<MonsterScriptableObject> pnjTeam;
}
