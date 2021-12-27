using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BattleState { START, PLAYERTURN, PLAYERTWOTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    [SerializeField] TMP_Text instructionsText, resultText, teamActionPointsText;
    [SerializeField] GameObject playerOne, playerTwo, resultPanel, KippoAvatar, OgamiAvatar;
    [SerializeField] GameObject[] enemys;
    public int attackOneDamage = 1;

    Unit playerOneUnit;
    Unit playerTwoUnit;

    EnemyUnit enemyOneUnit, enemyTwoUnit;

    EnemyProto enemyProto;

    PlayerMovementGrid playerMovementGrid;

    PlayerMovementGrid playerOnemovement, playerTwoMovement;

    public int enemyCount, playerCount;

    bool battleHasEnded = false;

    private void Awake()
    {
        playerMovementGrid = FindObjectOfType<PlayerMovementGrid>();
        enemyProto = FindObjectOfType<EnemyProto>();

        playerOnemovement = playerOne.GetComponent<PlayerMovementGrid>();
        playerTwoMovement = playerTwo.GetComponent<PlayerMovementGrid>();

        enemyCount = enemys.Length;
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
        if(enemyCount <= 0)
        {
            state = BattleState.WON;
           Invoke("MatchWon", 1f);
        }
        if(playerCount <= 0)
        {
            state = BattleState.LOST;
            MatchLost();
        }
        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            KippoAvatar.SetActive(true);
            playerOnemovement.PlayerPoints = 0;
        }
        else { KippoAvatar.SetActive(false); }
        if (!GameObject.FindGameObjectWithTag("Player2"))
        {
            OgamiAvatar.SetActive(true);
            playerTwoMovement.PlayerPoints = 0;
        }
        else { OgamiAvatar.SetActive(false); }

        if(playerOnemovement.PlayerPoints == 0 && playerTwoMovement.PlayerPoints == 0 && battleHasEnded == false)
        {
            state = BattleState.ENEMYTURN;
            EnemyTurn();
            playerMovementGrid.IsActiveToFalse();
        }
    }

    void SetupBattle()
    {
        playerOneUnit = playerOne.GetComponent<Unit>();
        playerTwoUnit = playerTwo.GetComponent<Unit>();

        enemyOneUnit = enemys[0].GetComponent<EnemyUnit>();
        //enemyTwoUnit = enemys[1].GetComponent<EnemyUnit>();

        state = BattleState.PLAYERTURN;
        PlayerOneTurn();
    }

    public void PlayerOneTurn()
    {
        state = BattleState.PLAYERTURN;
        instructionsText.text = playerOneUnit.unitName;
        if (!GameObject.FindGameObjectWithTag("Player") && !GameObject.FindGameObjectWithTag("Player2"))
        {
            MatchLost();
        }
        
    }

    public void PlayerTwoTurn()
    {
        state = BattleState.PLAYERTWOTURN;
        instructionsText.text = playerTwoUnit.unitName;
        if (!GameObject.FindGameObjectWithTag("Player2"))
        {
           EnemyTurn();
        }
    }

    public void EnemyTurn()
    {

        state = BattleState.ENEMYTURN;
        instructionsText.text = enemyOneUnit.enemyName;
        enemyProto.EnemyAction();
        playerOnemovement.ResetPlayerPoints();
        playerTwoMovement.ResetPlayerPoints();
    }

    public void MatchWon()
    {
        resultPanel.SetActive(true);
        resultText.text = "You Won This Match!";
        battleHasEnded = true;
        
    }

    public void MatchLost()
    {
        resultPanel.SetActive(true);
        resultText.text = "You Lost This Match!";
        battleHasEnded = true;
    }

    public void CountingEnemys()
    {
        enemyCount--;
    }
    public void CountingPlayers()
    {
        playerCount--;
    }
    
}