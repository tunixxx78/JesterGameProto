using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel, damage, maxHP, currentHp, playerActionPoints, newActionPoints;

    public int startDamage;

    public List<GameObject> fenses = new List<GameObject>();

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

        for (int i = 0; i < fenses.Count; i++)
        {
            fenses[i].GetComponent<StaticObstacle>().playerDamage = damage;

        }
        
        
    }
    public void DeCreaseAttackPower()
    {
        damage = startDamage;

        for (int i = 0; i < fenses.Count; i++)
        {
            fenses[i].GetComponent<StaticObstacle>().playerDamage = damage;

        }
        /*if (GameObject.Find("Chest"))
        {
            staticObstacle.realAttackDamage = 1;
            staticObstacle.playerDamage = 0;
        }*/
            
    }
}
