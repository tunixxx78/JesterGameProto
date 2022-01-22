using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySingleShoot : MonoBehaviour
{
    [Header("LEVEL DESIGNER USE!!!")]
    public float bulletRange; // Variable for bullet range in singleshot attack.
    [SerializeField] bool enemyTypeOne = false, enemyTypeTwo = false, hasAttackDelay = false;
    public int bulletDamage, attackDelay;

    [Header("PROGRAMMER USE!!!")]
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] GameObject enemyBullet;
    public int startAttackDealy;
    [SerializeField] Animator cannonAnimator;

    

    SFXManager sFXManager;

    public float currentSpeed, maxSpeed, minSpeed, accelerationTime, time; // variables for exponential speedUp for bullet.
    

    private void Awake()
    {
        sFXManager = FindObjectOfType<SFXManager>();
    }

    public void EnemySingleShootAction()
    {
        /*RaycastHit2D hitInfo = Physics2D.Raycast(bulletSpawnPoint.position, -bulletSpawnPoint.up);
        if (hitInfo)
        {
            Debug.Log(hitInfo.transform.name);
            PlayerMovementGrid player = hitInfo.transform.GetComponent<PlayerMovementGrid>();
            if (player != null)
            {

                player.PlayerTakeDamage(enemyUnit.damage);
            }
        }
        GameObject enemyShootingParticle = Instantiate(enemyBullet, bulletSpawnPoint.position, Quaternion.identity);
        Destroy(enemyShootingParticle, 1f);
         */

        //GameObject enemyBulletPrefab = Instantiate(enemyBullet, bulletSpawnPoint.position, Quaternion.identity);

        //sFXManager.enemyShoot.Play();
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
