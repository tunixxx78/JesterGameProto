using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_MoveCannonball : MonoBehaviour
{
    Vector3 startpos;
    Vector3 endPos;
    public float delta = 1f;
    [SerializeField] GameObject hitParticle;

    private void Start()
    {
        startpos = this.transform.position;
        endPos = new Vector3(startpos.x, startpos.y - 30, 0);
    }
    // Update is called once per frame
    void Update()
    {
        //Vector3 targetPos = Vector3.MoveTowards(startpos, endPos, delta);
        //this.transform.position = targetPos;
        transform.Translate(Vector2.down * delta * Time.deltaTime, Space.World);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player2" || collision.gameObject.tag == "DummuObstacle")
        {
            Instantiate(hitParticle, collision.transform.position, Quaternion.identity);

            this.enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
            Destroy(this.gameObject, 2);
        }
        /*if (collision.gameObject.tag == "BulletDestroyer")
        {
            Destroy(this.gameObject);
        }*/

        if (collision.gameObject.tag == "EnemyBulletDestroyer")
        {
            Destroy(this.gameObject);
        }
    }
}