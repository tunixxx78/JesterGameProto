using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyProto : MonoBehaviour
{
    [SerializeField] int enemyHealth, enemyStartHealt;
    [SerializeField] GameObject enemy, bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] Transform[] moveDirections;
    [SerializeField] float enemySpeed;
    EnemyUnit enemyUnit;

    [SerializeField] TMP_Text enemyHPText;

    BattleState state;

    GameManager gameManager;

    private void Awake()
    {
        enemyUnit = enemy.GetComponent<EnemyUnit>();
        enemyStartHealt = enemyUnit.enemyHP;
        enemyHealth = enemyStartHealt;
        enemyHPText.text = enemyHealth.ToString();

        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if(enemyHealth <= 0)
        {
            Destroy(this.gameObject);
        }

        enemyHPText.text = enemyHealth.ToString();

        
        
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
        Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        yield return new WaitForSeconds(1f);

        var randomDirection = Random.Range(0, moveDirections.Length);
        //transform.position = Vector3.MoveTowards(transform.position, moveDirections[randomDirection].position, 2f);

        yield return new WaitForSeconds(1f);


        gameManager.FromPTwoToPOne();
    }
}
