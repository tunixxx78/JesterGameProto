using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTile : MonoBehaviour
{
    public float damageMultiplier, startDamageAmount;
    public bool damageNumberIsNegative = false;

    [SerializeField] Animator attackTileAnimator;

    BattleSystem battleSystem;

    private void Awake()
    {
        battleSystem = FindObjectOfType<BattleSystem>();
        startDamageAmount = FindObjectOfType<Unit>().startDamage;
    }

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
