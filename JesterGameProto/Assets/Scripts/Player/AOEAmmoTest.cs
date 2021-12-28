using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEAmmoTest : MonoBehaviour
{
    [SerializeField] GameObject onHitParticle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(onHitParticle, collision.transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<Animator>().SetTrigger("TakeDmg");

            
        }
    }
}
