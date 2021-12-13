using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyProto : MonoBehaviour
{
    [SerializeField] int enemyHealth, enemyStartHealt;
    [SerializeField] GameObject enemy;
    EnemyUnit enemyUnit;

    [SerializeField] TMP_Text enemyHPText;

    private void Awake()
    {
        enemyUnit = enemy.GetComponent<EnemyUnit>();
        enemyStartHealt = enemyUnit.enemyHP;
        enemyHealth = enemyStartHealt;
        enemyHPText.text = enemyHealth.ToString();
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
}
