using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeTile : MonoBehaviour
{
    public float rangeAmount;
    public float starBulletRange;
    public bool RangeNumberIsNegative = false;

    [SerializeField] Animator attackTileAnimator;

    private void Awake()
    {
        starBulletRange = FindObjectOfType<SingleTargetAttack>().bulletRange;
    }

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
