using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPointTile : MonoBehaviour
{
    public float extraActionPoints;
    public bool actionpointIsNegative = false;

    [SerializeField] bool cantBeUsedMultipleTimes = false;

    [SerializeField] Animator attackTileAnimator;

    BattleSystem battleSystem;

    private void Awake()
    {
        battleSystem = FindObjectOfType<BattleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attackTileAnimator.SetBool("isActive", true);
        }

        if (collision.gameObject.tag == "Player" && cantBeUsedMultipleTimes)
        {
            Destroy(this.gameObject, 1f);
            battleSystem.FindActionPointTileStats();
        }

        if (actionpointIsNegative)
        {
            battleSystem.actionPAmount = extraActionPoints;
            battleSystem.actionPIsNegative = true;
        }
        if (actionpointIsNegative == false)
        {
            battleSystem.actionPAmount = extraActionPoints;
            battleSystem.actionPIsNegative = false;
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
