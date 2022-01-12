using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObstacle : MonoBehaviour
{
    
    public int playerDamage, realAttackDamage;

    int damageBoost;
    BattleSystem battleSystem;
    [SerializeField] GameObject destroyedObstacle, obstacle;

    [SerializeField] HealthBar obstacleHealthbar;
    [SerializeField] int obstacleHealt, obstacleStartHealth, normalHitDamage, boostedHitDamage;

    [SerializeField] GameObject[] players;

    [SerializeField] Transform destroyedObstacleSpawnPoint;

    private void Awake()
    {
        //battleSystem = FindObjectOfType<BattleSystem>();

        //playerDamage = battleSystem.boosteddamages;

        //Debug.Log("Player damage is " + playerDamage);

        obstacleHealt = obstacleStartHealth;
    }

    private void Start()
    {
        obstacleHealthbar.SetMaxValue(obstacleStartHealth);
        destroyedObstacleSpawnPoint.parent = null;
    }

    private void Update()
    {
        //playerDamage = battleSystem.attackOneDamage;

        //damageBoost = playerDamage;

        //playerDamage = damageBoost;
        realAttackDamage = playerDamage;

        if (obstacleHealt <= 0)
        {
            Debug.Log("HAISTA PASKA!!");
            GameObject destroyed = Instantiate(destroyedObstacle, destroyedObstacleSpawnPoint.position, Quaternion.identity);
            Destroy(destroyed, 2f);

            Destroy(this.gameObject);
            players[0].GetComponent<Unit>().fenses.Remove(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Bullet")
        {
            ObstacleDamage(normalHitDamage);

            if(realAttackDamage  >= 5)
            {
                ObstacleDamage(boostedHitDamage);
            }
        }
    }

    public void DamagePointsUp()
    {
        for (int i = 0; i < players.Length; i++)
        {
            damageBoost = players[i].GetComponent<Unit>().damage;
            Debug.Log(damageBoost);

            playerDamage += damageBoost;
            Debug.Log("Player boosteddamage is " + playerDamage);

            realAttackDamage = playerDamage;
        }
    }

    public void ObstacleDamage(int damage)
    {
        obstacleHealt -= damage;
        obstacleHealthbar.SetHealth(obstacleHealt);
    }
    
}
