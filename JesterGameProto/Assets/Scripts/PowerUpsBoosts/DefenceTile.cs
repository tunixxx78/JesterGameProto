using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceTile : MonoBehaviour
{
    public int armourAmount;

    [SerializeField] Animator attackTileAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
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
