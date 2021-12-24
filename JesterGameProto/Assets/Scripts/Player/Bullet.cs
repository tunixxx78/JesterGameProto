using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D ammoRB;
    [SerializeField] float ammoSpeed = 5f;

    private void Update()
    {
        ammoRB.velocity = transform.up * ammoSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("BulletDestroyer") || collision.collider.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
    
}
