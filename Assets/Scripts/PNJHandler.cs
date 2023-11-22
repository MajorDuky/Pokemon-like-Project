using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PNJHandler : MonoBehaviour
{
    [SerializeField] private PNJScriptableObject dataPNJ;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private BoxCollider2D detectionCollider;

    // Start is called before the first frame update
    void Start()
    {
        if (Mathf.Abs(dataPNJ.lookOrientation.x) > 0)
        {
            float offset = dataPNJ.lookOrientation.x < 0 ? sprite.size.x / 2 : -sprite.size.x / 2;
            detectionCollider.offset = new Vector2 (dataPNJ.visionRange / 2 - offset, 0);
            detectionCollider.size = new Vector2(Mathf.Abs(dataPNJ.lookOrientation.x * dataPNJ.visionRange), 1);
        }
        else
        {
            float offset = dataPNJ.lookOrientation.y < 0 ? sprite.size.y / 2 : -sprite.size.y / 2;
            detectionCollider.offset = new Vector2(0, dataPNJ.visionRange / 2 - offset);
            detectionCollider.size = new Vector2(1, Mathf.Abs(dataPNJ.lookOrientation.y * dataPNJ.visionRange));
        }
        sprite.sprite = dataPNJ.sprite;
        transform.position = dataPNJ.spawnLocation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // C'EST DU TEST BORDEL
        // FAUT QUE CE SOIT PLUS PROPRE CONNARD
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerTeamHandler player = FindObjectOfType<PlayerTeamHandler>();
            player.StartBattle(dataPNJ.pnjTeam);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
