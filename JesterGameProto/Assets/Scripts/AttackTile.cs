using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTile : MonoBehaviour
{
    [SerializeField] Transform spawnPointOne, spawnPointTwo;
    [SerializeField] GameObject spawnOne, spawnTwo;
    [SerializeField] GameObject ammoPrefab;
    bool canShoot = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
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
