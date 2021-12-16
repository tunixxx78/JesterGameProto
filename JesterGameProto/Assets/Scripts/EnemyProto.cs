using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyProto : MonoBehaviour
{
    [SerializeField] int enemyHealth, enemyStartHealt;
    [SerializeField] GameObject enemy, bulletPrefab, enemyAttackFX;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] Transform[] moveDirections;
    [SerializeField] float enemySpeed;
    EnemyUnit enemyUnit;

    [SerializeField] TMP_Text enemyHPText;

    BattleSystem battleSystem;

    GameManager gameManager;

    [SerializeField] HealthBar enemyHealthBar;

    private void Awake()
    {
        enemyUnit = enemy.GetComponent<EnemyUnit>();
        enemyStartHealt = enemyUnit.enemyHP;
        enemyHealth = enemyStartHealt;
        enemyHPText.text = enemyHealth.ToString();

        gameManager = FindObjectOfType<GameManager>();

        battleSystem = FindObjectOfType<BattleSystem>();
    }

    private void Start()
    {
        enemyHealthBar.SetMaxValue(enemyStartHealt);
    }

    private void Update()
    {
        if(enemyHealth <= 0)
        {
            battleSystem.CountingEnemys();

            Destroy(this.gameObject, .5f);
            
        }

        enemyHPText.text = enemyHealth.ToString();

        
        
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
            enemyHealth--;
        }
    }

    public void EnemyAction()
    {
        Debug.Log("VIHOLLINEN HY?KK??");
        StartCoroutine(EnemyAttack());
    }

    IEnumerator EnemyAttack()
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
        //transform.position = Vector3.MoveTowards(transform.position, moveDirections[randomDirection].position, 1f);

        yield return new WaitForSeconds(1f);


        gameManager.FromPTwoToPOne();
    }
}
