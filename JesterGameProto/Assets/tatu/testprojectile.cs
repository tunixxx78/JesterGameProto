using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testprojectile : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;
    SingleTargetAttack singleTargetAttack;

    SFXManager sFXManager;

    public GameObject onHitParticle;
    //[SerializeField]
    float currentSpeed, maxSpeed, minSpeed, accelerationTime, time; // variables for exponential speedUp for bullet.

    public float delta = 0.15f; //BulletSpeed

    private void Awake()
    {
        singleTargetAttack = FindObjectOfType<SingleTargetAttack>();
        sFXManager = FindObjectOfType<SFXManager>();
    }

    private void Start()
    {
        //startPos = this.transform.position;
        //endPos = new Vector3(startPos.x, startPos.y + 20, 0);

        // setting up bullet SpeedUp.
        minSpeed = currentSpeed;
        time = 0;

        // values from SingleTargetAttack script.
        minSpeed = singleTargetAttack.minSpeed;
        maxSpeed = singleTargetAttack.maxSpeed;
        accelerationTime = singleTargetAttack.accelerationTime;
    }
    void Update()
    {
        currentSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, time / accelerationTime);
        transform.position += transform.up * currentSpeed * Time.deltaTime;
        time += Time.deltaTime;

        //Vector3 movement = Vector3.MoveTowards(this.transform.position, endPos, delta);
        //this.transform.position = movement;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(onHitParticle, collision.transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<Animator>().SetTrigger("TakeDmg");
            sFXManager.hitFromBullet.Play();

            //disable projectile on hit and hide Sprite
            this.enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(this.gameObject, 2);
        }

        if(collision.gameObject.CompareTag("BulletDestroyer"))
        {
            Instantiate(onHitParticle, collision.transform.position, Quaternion.identity);
            this.enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(this.gameObject, 2);
        }

        if(collision.gameObject.tag == "Obstacle")
        {
            Instantiate(onHitParticle, collision.transform.position, Quaternion.identity);
            this.enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(this.gameObject, 2);
        }
    }
}
