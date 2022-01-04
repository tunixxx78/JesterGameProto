using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BattleState { START, PLAYERTURN, PLAYERTWOTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    [SerializeField] TMP_Text resultText, teamActionPointsText;
    [SerializeField] GameObject /*playerOne, playerTwo,*/ resultPanel, KippoAvatar, OgamiAvatar, MoveOnButton;
    //public GameObject[] enemys; //players;
    public List<GameObject> enemys = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();
    public int attackOneDamage = 1, attackTwoDamage;
    public int allPlayerPoints;
    int playersStats;

    Unit playerUnit;
    Unit playerOneUnit;
    Unit playerTwoUnit;

    EnemyUnit enemyOneUnit, enemyTwoUnit;

    EnemyProto enemyProto, enemyTwoProto;

    PlayerMovementGrid playerMovementGrid;

    PlayerMovementGrid playerOnemovement, playerTwoMovement;

    public int enemyCount, playerCount;

    bool battleHasEnded = false, enemyIsAttacking = false;

    AttackTile attackTile;

    private void Awake()
    {
        playerMovementGrid = FindObjectOfType<PlayerMovementGrid>();

        //enemyProto = FindObjectOfType<EnemyProto>();

        for (int i = 0; i < players.Count; i++)
        {
            playersStats = players[i].GetComponent<PlayerMovementGrid>().PlayerPoints;
            Debug.Log(playersStats);

            allPlayerPoints += playersStats; 
        }


        //playerOnemovement = players[0].GetComponent<PlayerMovementGrid>();
        //playerTwoMovement = players[1].GetComponent<PlayerMovementGrid>();

        enemyCount = enemys.Count;
        playerCount = players.Count;

       
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
            //playerOnemovement.PlayerPoints = 0;
        }
        else { KippoAvatar.SetActive(false); }
        if (!GameObject.FindGameObjectWithTag("Player2"))
        {
            OgamiAvatar.SetActive(true);
            //playerTwoMovement.PlayerPoints = 0;
        }
        else { OgamiAvatar.SetActive(false); }

        if(allPlayerPoints == 0 && battleHasEnded == false && enemyIsAttacking == false)
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurnIvoke());
            playerMovementGrid.IsActiveToFalse();
            enemyIsAttacking = true;
        }
        /*if(playerOnemovement.PlayerPoints == 0 && playerTwoMovement.PlayerPoints == 0 && battleHasEnded == false)
        {
            state = BattleState.ENEMYTURN;
            EnemyTurn();
            playerMovementGrid.IsActiveToFalse();
        }*/

        

        attackOneDamage = playerUnit.damage;
    }

    void SetupBattle()
    {
        for (int i = 0; i < players.Count; i++)
        {
            playerUnit = players[0].GetComponent<Unit>();
        }

        //playerOneUnit = players[0].GetComponent<Unit>();
        //playerTwoUnit = players[1].GetComponent<Unit>();

        enemyOneUnit = enemys[0].GetComponent<EnemyUnit>();
        //enemyTwoUnit = enemys[1].GetComponent<EnemyUnit>();

        enemyProto = enemys[0].GetComponent<EnemyProto>();
        //enemyTwoProto = enemys[1].GetComponent<EnemyProto>();

        state = BattleState.PLAYERTURN;
        PlayerOneTurn();
    }

    public void PlayerOneTurn()
    {
        state = BattleState.PLAYERTURN;
        //instructionsText.text = playerOneUnit.unitName;
        if (!GameObject.FindGameObjectWithTag("Player") && !GameObject.FindGameObjectWithTag("Player2"))
        {
            MatchLost();
        }
        
    }

    public void PlayerTwoTurn()
    {
        state = BattleState.PLAYERTWOTURN;
        //instructionsText.text = playerTwoUnit.unitName;
        if (!GameObject.FindGameObjectWithTag("Player2"))
        {
           EnemyTurn();
        }
    }

    public void EnemyTurn()
    {

        //state = BattleState.ENEMYTURN;
        //instructionsText.text = enemyOneUnit.enemyName;
        
        for (int i = 0; i < enemys.Count; i++)
        {
            enemys[i].GetComponent<EnemyProto>().EnemyAction();
        }    
        //enemyProto.EnemyAction();
        //enemyTwoProto.EnemyAction();


        //playerOnemovement.ResetPlayerPoints();
        //playerTwoMovement.ResetPlayerPoints();
        enemyIsAttacking = true;

        StartCoroutine(ResetPlayerActionPoints());
        
    }

    public void MatchWon()
    {

        StartCoroutine(Won());
        
    }

    public void MatchLost()
    {
        StartCoroutine(Lost());
    }

    public void CountingEnemys()
    {
        enemyCount--;
    }
    public void CountingPlayers()
    {
        playerCount--;
    }

    IEnumerator ResetPlayerActionPoints()
    {
        yield return new WaitForSeconds(2);

        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerMovementGrid>().ResetPlayerPoints();

            playersStats = players[i].GetComponent<Unit>().newActionPoints;
            Debug.Log(playersStats);


            allPlayerPoints += playersStats;

            enemyIsAttacking = false;
        }
    }

    IEnumerator EnemyTurnIvoke()
    {
        yield return new WaitForSeconds(2);

        EnemyTurn();
        
    }

    IEnumerator Won()
    {
        yield return new WaitForSeconds(2);

        resultPanel.SetActive(true);
        MoveOnButton.SetActive(true);
        resultText.text = "You Won This Match!";
        battleHasEnded = true;
    }

    IEnumerator Lost()
    {
        yield return new WaitForSeconds(2);

        resultPanel.SetActive(true);
        resultText.text = "You Lost This Match!";
        battleHasEnded = true;
    }

    
}
