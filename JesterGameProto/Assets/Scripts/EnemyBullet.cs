using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D ammoRB;
    [SerializeField] float ammoSpeed = 5f;

    private void Update()
    {
        ammoRB.velocity = transform.up * -ammoSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("BulletDestroyer") || collision.collider.CompareTag("Player") || collision.collider.CompareTag("Player2"))
        {
            Destroy(this.gameObject);
        }
    }
    
}
