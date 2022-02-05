using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] GameObject hitParticle;

    float currentSpeed, maxSpeed, minSpeed, accelerationTime, time; // variables for exponential speedUp for bullet.
    float bulletRange; // Variable for bullet range in singleshot attack.

    EnemySingleShoot enemySingleShoot;

    SFXManager sFXManager;

    private void Awake()
    {
        enemySingleShoot = FindObjectOfType<EnemySingleShoot>();
        sFXManager = FindObjectOfType<SFXManager>();
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

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2")
        {
            Instantiate(hitParticle, collision.transform.position, Quaternion.identity);
            sFXManager.hitFromBullet.Play();

            this.enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(this.gameObject, 2);
            Destroy(this.hitParticle, 2f);
        }


        if(collision.gameObject.tag == "EnemyBulletDestroyer")
        {
            Instantiate(hitParticle, collision.transform.position, Quaternion.identity);
            this.enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(this.gameObject, 2);
            Destroy(this.hitParticle, 2f);
        }
        if(collision.gameObject.tag == "DummuObstacle")
        {
            Instantiate(hitParticle, collision.transform.position, Quaternion.identity);
            this.enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(this.gameObject, 2);
            Destroy(this.hitParticle, 2f);
        }
    }

}
