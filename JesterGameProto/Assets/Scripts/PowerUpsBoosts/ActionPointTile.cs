using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPointTile : MonoBehaviour
{
    public int extraActionPoints;

    [SerializeField] bool cantBeUsedMultipleTimes = false;

    [SerializeField] Animator attackTileAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attackTileAnimator.SetBool("isActive", true);
        }

        if (collision.gameObject.tag == "Player" && cantBeUsedMultipleTimes)
        {
            Destroy(this.gameObject, 1f);
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
