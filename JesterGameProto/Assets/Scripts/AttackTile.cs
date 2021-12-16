using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTile : MonoBehaviour
{
    [SerializeField] Transform spawnPointOne, spawnPointTwo;
    [SerializeField] GameObject spawnOne, spawnTwo;
    [SerializeField] GameObject ammoPrefab;
    bool canShoot = false;

    Unit playerUnit;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
           /* RaycastHit2D hitInfo = Physics2D.Raycast(spawnPointOne.position, spawnPointOne.up);
            RaycastHit2D hitInfoTwo = Physics2D.Raycast(spawnPointTwo.position, spawnPointTwo.up);
            if (hitInfo && hitInfoTwo)
            {
                Debug.Log(hitInfo.transform.name);
                Debug.Log(hitInfoTwo.transform.name);

                EnemyProto enemy = hitInfo.transform.GetComponent<EnemyProto>();
                EnemyProto enemyTwo = hitInfoTwo.transform.GetComponent<EnemyProto>();
                if (enemy != null)
                {

                    enemy.TakeDamage(playerUnit.damage);
                }
                if (enemyTwo != null)
                {
                    enemyTwo.TakeDamage(playerUnit.damage);
                }
            }*/

            Instantiate(ammoPrefab, spawnPointOne.position, Quaternion.identity);
            Instantiate(ammoPrefab, spawnPointTwo.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player" || collider.gameObject.tag == "Player2")
        {
            spawnOne.SetActive(true);
            spawnTwo.SetActive(true);

            canShoot = true;
            
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Player2")
        {
            spawnOne.SetActive(false);
            spawnTwo.SetActive(false);

            canShoot = false;

        }
    }
}
