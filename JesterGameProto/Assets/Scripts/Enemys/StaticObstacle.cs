using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObstacle : MonoBehaviour
{
    [Header("Level designer use!!!")]

    [Tooltip("Amount that obstacle has HEALTH! ")] [SerializeField] int obstacleStartHealth;
    [Tooltip("Amount that player causes damage with one shot! ")] [SerializeField] int normalHitDamage;


    [HideInInspector]
    [SerializeField] GameObject destroyedObstacle, obstacle;
    [HideInInspector]
    [SerializeField] HealthBar obstacleHealthbar;
    [HideInInspector]
    [SerializeField] int obstacleHealt;
    [HideInInspector]
    [SerializeField] Transform destroyedObstacleSpawnPoint;

    private void Awake()
    {
        obstacleHealt = obstacleStartHealth;
    }

    private void Start()
    {
        obstacleHealthbar.SetMaxValue(obstacleStartHealth);
        destroyedObstacleSpawnPoint.parent = null;
    }

    private void Update()
    {
        if (obstacleHealt <= 0)
        {
            
            GameObject destroyed = Instantiate(destroyedObstacle, destroyedObstacleSpawnPoint.position, Quaternion.identity);
            Destroy(destroyed, 7f);
            
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.gameObject.tag == "Bullet")
        {
            ObstacleDamage(normalHitDamage);
        }
    }
    
    public void ObstacleDamage(int damage)
    {
        obstacleHealt -= damage;
        obstacleHealthbar.SetHealth(obstacleHealt);
    }
    
}
