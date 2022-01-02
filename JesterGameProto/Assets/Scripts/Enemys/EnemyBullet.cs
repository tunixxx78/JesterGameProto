using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D ammoRB;
    [SerializeField] float ammoSpeed = 5f;
    [SerializeField] GameObject hitParticle;

    private void Update()
    {
        ammoRB.velocity = transform.up * -ammoSpeed * Time.deltaTime;
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("BulletDestroyer") || collision.collider.CompareTag("Player") || collision.collider.CompareTag("Player2"))
        {
            Instantiate(hitParticle, collision.transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            Instantiate(hitParticle, collision.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if(collision.gameObject.tag == "BulletDestroyer")
        {
            Destroy(this.gameObject);
        }
    }

}
