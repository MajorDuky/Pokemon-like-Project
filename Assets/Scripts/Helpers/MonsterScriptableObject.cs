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
    public bool isInNecronomicon;
    public float submissionRate;

    public void GainXp(float amountToAdd)
    {
        currentXp += amountToAdd;
        if (currentXp >= xpToLevelUp)
        {
            LevelUp(1);
        }
    }

    public void LevelUp(int lvlToGain)
    {
        float deltaXp = currentXp - xpToLevelUp;
        deltaXp = deltaXp < 0 ? 0 : deltaXp;
        currentXp = deltaXp;
        level += lvlToGain;
        for (int i = 0; i < lvlToGain; i++)
        {
            xpToLevelUp *= levelUpXpMultiplier;
            maxHealth *= levelUpHealthMultiplier;
            maxSpiritPower *= levelUpSPMultiplier;
        }
        Revive();
        RefillSP();
        if (isAlly)
        {
            GameManager.Instance.IncreaseBaseXPGain();
        }
    }

    public void TakeDamage(float amount)
    {
        if (amount > health)
        {
            health = 0;
        }
        else
        {
            health -= amount;
        }
        
        if (health <= 0)
        {
            isAlive = false;
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

    public void Revive()
    {
        isAlive = true;
        health = maxHealth;
    }
}