using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    PlayerMovementGrid playerMovementGrid;

    BattleState state;

    private void Awake()
    {
        playerMovementGrid = FindObjectOfType<PlayerMovementGrid>();
    }

    public void EndOfTurn()
    {
        playerMovementGrid.IsActiveToFalse();
    }

    public void FromPOneToPTwo()
    {
        FindObjectOfType<BattleSystem>().PlayerTwoTurn();
    }
    public void FromPTwoToPOne()
    {
        FindObjectOfType<BattleSystem>().PlayerOneTurn();
    }

    public void PlayerAttack()
    {
        //FindObjectOfType<PlayerMovementGrid>().Attack();
    }
    public void EnemyTurn()
    {
        FindObjectOfType<BattleSystem>().EnemyTurn();
    }
}
