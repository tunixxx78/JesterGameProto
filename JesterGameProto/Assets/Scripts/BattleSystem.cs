using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BattleState { START, PLAYERTURN, PLAYERTWOTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    [SerializeField] TMP_Text instructionsText;
    [SerializeField] GameObject playerOne, playerTwo, enemyOne;

    Unit playerOneUnit;
    Unit playerTwoUnit;
    EnemyUnit enemyOneUnit;

    EnemyProto enemyProto;

    private void Awake()
    {
        enemyProto = FindObjectOfType<EnemyProto>();
    }

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    private void Update()
    {
        if (state == BattleState.ENEMYTURN)
        {
            //enemyProto.EnemyAction();
           
        }
    }

    void SetupBattle()
    {
        playerOneUnit = playerOne.GetComponent<Unit>();
        playerTwoUnit = playerTwo.GetComponent<Unit>();

        enemyOneUnit = enemyOne.GetComponent<EnemyUnit>();
        state = BattleState.PLAYERTURN;
        PlayerOneTurn();
    }

    public void PlayerOneTurn()
    {
        state = BattleState.PLAYERTURN;
        instructionsText.text = playerOneUnit.unitName;
        
    }

    public void PlayerTwoTurn()
    {
        state = BattleState.PLAYERTWOTURN;
        instructionsText.text = playerTwoUnit.unitName;
    }

    public void EnemyTurn()
    {
        state = BattleState.ENEMYTURN;
        instructionsText.text = enemyOneUnit.enemyName;
        enemyProto.EnemyAction();
    }
    
}
