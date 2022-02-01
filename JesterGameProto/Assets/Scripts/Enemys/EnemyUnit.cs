using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    [Header("LEVEL DESIGNER USE!!!")]
    [Tooltip("Add wanted enemy HEALTH here! ")] public int enemyHP;

    
    [Header("PROGRAMMER USE!!!")]
    public int damage;
    [HideInInspector]
    public int range;
}
