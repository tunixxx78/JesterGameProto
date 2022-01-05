using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTile : MonoBehaviour
{
    public int damageMultiplier;
    [SerializeField] Animator attackTileAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            attackTileAnimator.SetBool("isActive", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attackTileAnimator.SetBool("isActive", false);
        }
    }
}
