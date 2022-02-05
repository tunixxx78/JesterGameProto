using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySingleShoot : MonoBehaviour
{
    [Header("LEVEL DESIGNER USE!!!")]
    public float bulletRange; // Variable for bullet range in singleshot attack.
    [Tooltip(" If attached to CANNON, this needs to be true, otherwise false ! ")] [SerializeField] bool enemyTypeOne = false;
    [Tooltip(" If attached to MOVING ENEMY, this needs to be true, otherwise false ! ")] [SerializeField] bool enemyTypeTwo = false;
    [Tooltip(" If enemy has attack delay, this needs to be True. Otherwise false! ")] [SerializeField] bool hasAttackDelay = false;
    public int bulletDamage, startAttackDelay;

   [HideInInspector]
    [Header("PROGRAMMER USE!!!")]
    [SerializeField] Transform bulletSpawnPoint;
    [HideInInspector]
    [SerializeField] GameObject enemyBullet;
    [HideInInspector]
    public int attackDelay;
    //[HideInInspector]
    [SerializeField] Animator cannonAnimator;

    

    SFXManager sFXManager;
    [HideInInspector]
    public float currentSpeed, maxSpeed, minSpeed, accelerationTime, time; // variables for exponential speedUp for bullet.
    

    private void Awake()
    {
        sFXManager = FindObjectOfType<SFXManager>();
        attackDelay = startAttackDelay;
    }

    public void EnemySingleShootAction()
    {
       // this checks if attackDelay is allowed and whitch kind of attack is sellected

        if(attackDelay <= 0 && hasAttackDelay)
        {
            if (enemyTypeOne)
            {
                cannonAnimator.SetTrigger("Shoot");
            }


            if (enemyTypeTwo)
            {
                cannonAnimator.SetTrigger("Attack");
            }
        }
        else if(hasAttackDelay == false)
        {
            if (enemyTypeOne)
            {
                cannonAnimator.SetTrigger("Shoot");
            }


            if (enemyTypeTwo)
            {
                cannonAnimator.SetTrigger("Attack");
            }
        }
        
    }
}
