using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel, damage, maxHP, currentHp, playerActionPoints, newActionPoints;

    public int startDamage; 

    AttackTile attackTile;
    StaticObstacle staticObstacle;

    private void Awake()
    {
        attackTile = FindObjectOfType<AttackTile>();
        damage = startDamage;

        staticObstacle = FindObjectOfType<StaticObstacle>();
    }

    public void InCreaseAttackPower()
    {
        damage = damage + attackTile.damageMultiplier;
        if (GameObject.Find("Chest"))
        {
            staticObstacle.DamagePointsUp();
        }
        
    }
    public void DeCreaseAttackPower()
    {
        damage = startDamage;
        if (GameObject.Find("Chest"))
        {
            staticObstacle.realAttackDamage = 1;
            staticObstacle.playerDamage = 0;
        }
            
    }
}
