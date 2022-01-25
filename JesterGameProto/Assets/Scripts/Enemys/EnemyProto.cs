using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyProto : MonoBehaviour
{
    [Header("LEVEL DESIGNER USE !!!")]
    [SerializeField] float enemyHealth; 
    [SerializeField] float enemyStartHealt;
    [SerializeField] bool hasMovementScript = false, friendlyFire = false;

    [Header("PROGRAMMER USE!!!")]

    [SerializeField] GameObject enemy; 
    [SerializeField] GameObject destroyedEnemy;
    public GameObject inTargetIcon;
    [SerializeField] Transform[] moveDirections;
    [SerializeField] float enemySpeed, ammoRange;
    [SerializeField] Animator enemyAnimator;

    [SerializeField] Transform bulletTargetRange, destroyedEnemySpawnPoint;

    EnemyUnit enemyUnit;

    [SerializeField] TMP_Text EnemyAttackDelayText;

    BattleSystem battleSystem;

    GameManager gameManager;

    [SerializeField] HealthBar enemyHealthBar;

    SFXManager sFXManager;

    EnemySingleShoot enemySingleShoot;


    private void Awake()
    {
        enemyUnit = enemy.GetComponent<EnemyUnit>();
        enemyStartHealt = enemyUnit.enemyHP;
        enemyHealth = enemyStartHealt;

        gameManager = FindObjectOfType<GameManager>();

        battleSystem = FindObjectOfType<BattleSystem>();

        sFXManager = FindObjectOfType<SFXManager>();

        enemySingleShoot = FindObjectOfType<EnemySingleShoot>();

        ammoRange = enemySingleShoot.bulletRange / 2;
        
        
        
        
    }

    private void Start()
    {
        enemyHealthBar.SetMaxValue(enemyStartHealt);

        destroyedEnemySpawnPoint.parent = null;

        sFXManager.enemyBounce.Play();

        bulletTargetRange.transform.position = bulletTargetRange.transform.position + new Vector3(0, -ammoRange, 0);
    }

    private void Update()
    {
        if(enemyHealth <= 0)
        {
            battleSystem.CountingEnemys();

            GameObject destroyed = Instantiate(destroyedEnemy, destroyedEnemySpawnPoint.position, Quaternion.identity);
            Destroy(destroyed, 7f);

            battleSystem.enemys.Remove(this.gameObject);

            Destroy(this.gameObject);
            
        }

        EnemyAttackDelayText.text = GetComponent<EnemySingleShoot>().attackDelay.ToString();

        
        
    }

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        enemyHealthBar.SetHealth(enemyHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            //enemyHealth--;
            TakeDamage(battleSystem.attackOneDamage);
            enemyAnimator.SetTrigger("TakeDamage");
            Debug.Log("OSUMA TULI!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            TakeDamage(battleSystem.attackOneDamage);
            enemyAnimator.SetTrigger("TakeDamage");
            Debug.Log("DAMAGEEEEEEEEE" + battleSystem.attackOneDamage);
        }
        if(collision.gameObject.tag == "Bullet2")
        {
            TakeDamage(battleSystem.attackTwoDamage);
            enemyAnimator.SetTrigger("TakeDamage");
        }
        if (friendlyFire) //checks if friendly fire is allowed
        {
            if (collision.gameObject.tag == "EnemyBullet")
            {
                TakeDamage(battleSystem.attackTwoDamage);
                enemyAnimator.SetTrigger("TakeDamage");
            }
        }
        
    }
    public void EnemyAction()
    {
        //enemy shoots and after that checks if there is moving script attached
        
        GetComponent<EnemySingleShoot>().EnemySingleShootAction();

        if(hasMovementScript)
        {
            StartCoroutine(MoveDumbEnemy());
        }
        //StartCoroutine(EnemyAttack());
    }
    IEnumerator MoveDumbEnemy()
    {
        yield return new WaitForSeconds(2);

        FindObjectOfType<EnemyDumbMovement>().DumbEnemyMovement();
    }

}
