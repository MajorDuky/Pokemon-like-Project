using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "ScriptableObjects/MonsterScriptableObject", order = 3)]
public class MonsterScriptableObject : ScriptableObject
{
    public string monsterName;
    public int level;
    public float currentXp;
    public float xpToLevelUp;
    public float levelUpXpMultiplier;
    public List<CapacityScriptableObject> capacitiesList;
    public List<TypeScriptableObject> typesList;
    public List<TypeScriptableObject> strengthsList;
    public List<TypeScriptableObject> weaknessesList;
    public List<TypeScriptableObject> ignoreDmgList;
    public float health;
    public float maxHealth;
    public float levelUpHealthMultiplier;
    public float spiritPower;
    public float maxSpiritPower;
    public float levelUpSPMultiplier;
    public Sprite frontSprite;
    public Sprite backSprite;
    public float speed;
    public bool isAlive;
    public bool isAlly;

    public void GainXp(float amountToAdd)
    {
        currentXp += amountToAdd;
        if (currentXp >= xpToLevelUp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        float deltaXp = currentXp - xpToLevelUp;
        currentXp = deltaXp;
        level++;
        xpToLevelUp *= levelUpXpMultiplier;
        maxHealth *= levelUpHealthMultiplier;
        maxSpiritPower *= levelUpSPMultiplier;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Debug.Log("Negetive Life Coucou");
            // BattleManager->gameover
        }
    }

    public void Heal(float amount)
    {
        health = health + amount >= maxHealth ? maxHealth : health += amount;
    }

    public void ReduceSP(float amount)
    {
        spiritPower -= amount;
    }

    public void RefillSP()
    {
        spiritPower = maxSpiritPower;
    }
}