using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "BuildingData", menuName = "ScriptableObjects/BuildingScriptableObject", order = 1)]
public class BuildingScriptableObject : ScriptableObject
{
    public string prefabName;
    public List<TileBase> tiles;
    public Vector3 doorLocation;
    public GameObject ground;
    public GameObject obstacles;
    public GameObject exitPoint;
    public Transform spawnPoint;
    public List<PNJScriptableObject> pnjs;
}
