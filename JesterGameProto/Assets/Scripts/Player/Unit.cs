using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel, damage, maxHP, currentHp, playerActionPoints, newActionPoints;

    public int startDamage;

    AttackTile attackTile;

    private void Awake()
    {
        attackTile = FindObjectOfType<AttackTile>();
        damage = startDamage;
    }

    public void InCreaseAttackPower()
    {
        damage = damage + attackTile.damageMultiplier;
    }
    public void DeCreaseAttackPower()
    {
        damage = startDamage;
    }
}
