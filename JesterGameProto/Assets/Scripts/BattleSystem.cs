using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BattleState { START, PLAYERTURN, PLAYERTWOTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    [SerializeField] TMP_Text instructionsText, resultText;
    [SerializeField] GameObject playerOne, playerTwo, enemyOne, resultPanel, KippoAvatar, OgamiAvatar;

    public int TeamActionPoints = 10;

    Unit playerOneUnit;
    Unit playerTwoUnit;
    EnemyUnit enemyOneUnit;

    EnemyProto enemyProto;

    [SerializeField] int enemyCount, playerCount;

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
        }
        else { KippoAvatar.SetActive(false); }
        if (!GameObject.FindGameObjectWithTag("Player2"))
        {
            OgamiAvatar.SetActive(true);
        }
        else { OgamiAvatar.SetActive(false); }
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
        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            PlayerTwoTurn();
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
    }

    public void MatchWon()
    {
        resultPanel.SetActive(true);
        resultText.text = "You Won This Match!";
    }

    public void MatchLost()
    {
        resultPanel.SetActive(true);
        resultText.text = "You Lost This Match!";
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
