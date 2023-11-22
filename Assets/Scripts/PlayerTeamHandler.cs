using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeamHandler : MonoBehaviour
{
    public List<MonsterScriptableObject> playerTeam;
    public BattleManager battleManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBattle(List<MonsterScriptableObject> enemyTeam)
    {
        battleManager.gameObject.SetActive(true);
        battleManager.InitializeBattle(enemyTeam);
    }
}
