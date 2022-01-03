using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] GameObject hitParticle;

    float currentSpeed, maxSpeed, minSpeed, accelerationTime, time; // variables for exponential speedUp for bullet.
    float bulletRange; // Variable for bullet range in singleshot attack.

    EnemySingleShoot enemySingleShoot;

    private void Awake()
    {
        enemySingleShoot = FindObjectOfType<EnemySingleShoot>();
    }

    private void Start()
    {
        // setting up bullet SpeedUp.
        minSpeed = currentSpeed;
        time = 0;

        // values from SingleTargetAttack script.
        minSpeed = enemySingleShoot.minSpeed;
        maxSpeed = enemySingleShoot.maxSpeed;
        accelerationTime = enemySingleShoot.accelerationTime;
    }

    private void Update()
    {
        currentSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, time / accelerationTime);
        transform.position += -transform.up * currentSpeed * Time.deltaTime;
        time += Time.deltaTime;

        //ammoRB.velocity = transform.up * -ammoSpeed * Time.deltaTime;
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

        if(collision.gameObject.tag == "EnemyBulletDestroyer")
        {
            Instantiate(hitParticle, collision.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}
