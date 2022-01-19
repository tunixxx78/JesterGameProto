using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeTile : MonoBehaviour
{
    public float rangeAmount;
    public float starBulletRange;
    public bool RangeNumberIsNegative = false;

    [SerializeField] Animator attackTileAnimator;

    BattleSystem battleSystem;

    private void Awake()
    {
        starBulletRange = FindObjectOfType<SingleTargetAttack>().bulletRange;
        battleSystem = FindObjectOfType<BattleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attackTileAnimator.SetBool("isActive", true);
        }
        if (RangeNumberIsNegative)
        {
            battleSystem.rangeTileAmount = rangeAmount;
            battleSystem.rangeIsNegative = true;

        }
        if (RangeNumberIsNegative == false)
        {
            battleSystem.rangeTileAmount = rangeAmount;
            battleSystem.rangeIsNegative = false;
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
