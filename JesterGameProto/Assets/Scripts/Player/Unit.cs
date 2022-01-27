using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("LEVEL DESIGNER USE!!!")]

    [Tooltip(" Insert wanted player maximum HEALTH here! ")] public int maxHP;    
    [Tooltip(" Insert wanted player ACTION POINTS here! ")] public int playerActionPoints;
    [Tooltip(" Insert wanted player DAMAGE here! ")] public int startDamage;
    [Tooltip(" Insert wanted COST FOR PLAYER ATTACK here! ")] public float AttackCost;
    [Tooltip(" Drag all FENCES in level here! ")] public List<GameObject> fenses = new List<GameObject>();


    [HideInInspector] public int damage;
    [HideInInspector] public int newActionPoints;
    [HideInInspector] public int currentHp;

    AttackTile attackTile;
    StaticObstacle staticObstacle;

    private void Awake()
    {
        attackTile = FindObjectOfType<AttackTile>();
        damage = startDamage;

        staticObstacle = FindObjectOfType<StaticObstacle>();

        newActionPoints = playerActionPoints;
    }

    public void InCreaseAttackPower()
    {
        //damage = damage + attackTile.damageMultiplier;

        for (int i = 0; i < fenses.Count; i++)
        {
            fenses[i].GetComponent<StaticObstacle>().playerDamage = damage;

        }
        
        
    }
    public void DeCreaseAttackPower()
    {
        //damage = startDamage;

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
