using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    PlayerMovementGrid playerMovementGrid;

    private void Awake()
    {
        playerMovementGrid = FindObjectOfType<PlayerMovementGrid>();
    }

    public void EndOfTurn()
    {
        playerMovementGrid.IsActiveToFalse();
    }
}
