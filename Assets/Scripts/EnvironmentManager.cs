using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnvironmentManager : MonoBehaviour
{
    public static EnvironmentManager Instance { get; private set; }
    public Tilemap defaultEnvironment;
    public Tilemap defaultObstacles;
    public Tilemap defaultInteractables;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    /// <summary>
    /// Method that is used to switch tilemaps when entering a new building / area
    /// </summary>
    /// <param name="environment">The Gameobject that contains the new environment tilemap</param>
    /// <param name="obstacles">The Gameobject that contains the new obstacles tilemap</param>
    public void SwitchTilemaps(GameObject environment, GameObject obstacles)
    {
        defaultEnvironment.gameObject.SetActive(false);
        defaultObstacles.gameObject.SetActive(false);
        Instantiate(environment, transform);
        Instantiate(obstacles, transform);
    }
}
