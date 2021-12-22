using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testprojectile : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;

    public GameObject onHitParticle;
    


    public float delta = 0.15f;
    private void Start()
    {
        startPos = this.transform.position;
        endPos = new Vector3(startPos.x, startPos.y + 20, 0);
    }
    void Update()
    {
        Vector3 movement = Vector3.MoveTowards(this.transform.position, endPos, delta);
        this.transform.position = movement;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(onHitParticle, collision.transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<Animator>().SetTrigger("TakeDmg");

            //disable projectile on hit and hide Sprite
            this.enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(this.gameObject, 2);
        }
    }
}
