using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyProto : MonoBehaviour
{
    [SerializeField] int enemyHealth, enemyStartHealt;
    [SerializeField] GameObject enemy; //bulletPrefab, enemyAttackFX;
    public GameObject inTargetIcon;
    //[SerializeField] Transform bulletSpawnPoint;
    [SerializeField] Transform[] moveDirections;
    [SerializeField] float enemySpeed, ammoRange;

    [SerializeField] Transform bulletTargetRange;

    EnemyUnit enemyUnit;

    //[SerializeField] TMP_Text enemyHPText;

    BattleSystem battleSystem;

    GameManager gameManager;

    [SerializeField] HealthBar enemyHealthBar;

    SFXManager sFXManager;

    EnemySingleShoot enemySingleShoot;

    [SerializeField] bool hasMovementScript = false, friendlyFire = false;

    //public Animator enemyAnimator;

    private void Awake()
    {
        enemyUnit = enemy.GetComponent<EnemyUnit>();
        enemyStartHealt = enemyUnit.enemyHP;
        enemyHealth = enemyStartHealt;
        //enemyHPText.text = enemyHealth.ToString();

        gameManager = FindObjectOfType<GameManager>();

        battleSystem = FindObjectOfType<BattleSystem>();

        sFXManager = FindObjectOfType<SFXManager>();

        enemySingleShoot = FindObjectOfType<EnemySingleShoot>();

        ammoRange = enemySingleShoot.bulletRange / 2;
        
        
        
        
    }

    private void Start()
    {
        enemyHealthBar.SetMaxValue(enemyStartHealt);

        sFXManager.enemyBounce.Play();

        bulletTargetRange.transform.position = bulletTargetRange.transform.position + new Vector3(0, -ammoRange, 0);
    }

    private void Update()
    {
        if(enemyHealth <= 0)
        {
            battleSystem.CountingEnemys();

            battleSystem.enemys.Remove(this.gameObject);

            Destroy(this.gameObject);
            
        }

        //enemyHPText.text = enemyHealth.ToString();

        
        
    }

    public void TakeDamage(int damage)
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
            Debug.Log("OSUMA TULI!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            TakeDamage(battleSystem.attackOneDamage);
            Debug.Log("DAMAGEEEEEEEEE" + battleSystem.attackOneDamage);
        }
        if(collision.gameObject.tag == "Bullet2")
        {
            TakeDamage(battleSystem.attackTwoDamage);
        }
        if (friendlyFire)
        {
            if (collision.gameObject.tag == "EnemyBullet")
            {
                TakeDamage(battleSystem.attackTwoDamage);
            }
        }
        
    }
    public void EnemyAction()
    {
        Debug.Log("VIHOLLINEN HY?KK??");
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


    // Enemy "AI" functionality

    /*IEnumerator EnemyAttack()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(bulletSpawnPoint.position, -bulletSpawnPoint.up);
        if (hitInfo)
        {
            Debug.Log(hitInfo.transform.name);
            PlayerMovementGrid player = hitInfo.transform.GetComponent<PlayerMovementGrid>();
            if (player != null)
            {

                player.PlayerTakeDamage(enemyUnit.damage);
            }
        }
        GameObject enemyShootingParticle = Instantiate(enemyAttackFX, bulletSpawnPoint.position, Quaternion.identity);
        Destroy(enemyShootingParticle, 1f);

        yield return new WaitForSeconds(1f);


        var randomDirection = Random.Range(0, moveDirections.Length);

        if(transform.position.x <= -.9)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveDirections[0].position, 1f);
        }
        if (transform.position.x >= .9)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveDirections[1].position, 1f);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, moveDirections[randomDirection].position, 1f);
        }
        

        yield return new WaitForSeconds(1f);

        
        gameManager.FromPTwoToPOne();
    }
    */
}
