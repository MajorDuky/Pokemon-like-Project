using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingManager : MonoBehaviour
{
    public BuildingScriptableObject building;
    [SerializeField] private GameObject grid;
    private BoxCollider2D boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        transform.position = building.doorLocation + new Vector3(0.5f, 0.5f, 0f);
        PaintBuiding();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Method that paints the building's tiles on the designated tilemaps
    /// </summary>
    void PaintBuiding()
    {
        Vector3 startPaintPosition = building.doorLocation + new Vector3(-1, 2);
        Vector3 endPaintPosition = building.doorLocation + new Vector3(1, 0);
        int tileNumber = 0;
        for (int y = (int)startPaintPosition.y; y > (int)endPaintPosition.y - 1; y--)
        {
            for (int x = (int)startPaintPosition.x; x < (int)endPaintPosition.x + 1; x++)
            {
                if (tileNumber == 7)
                {
                    EnvironmentManager.Instance.defaultInteractables.SetTile(new Vector3Int(x, y, 0), building.tiles[tileNumber]);
                }
                else
                {
                    EnvironmentManager.Instance.defaultObstacles.SetTile(new Vector3Int(x, y, 0), building.tiles[tileNumber]);
                }
                tileNumber++;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EnvironmentManager.Instance.SwitchTilemaps(building.ground, building.obstacles, building.exitPoint);
            other.gameObject.transform.position = building.spawnPoint.position;
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.TPMovePoint();
                playerMovement.playerPosBeforeEnteringBuilding = new Vector3(0.5f, -0.5f, 0f) + building.doorLocation;
            }
        }   
    }
}
