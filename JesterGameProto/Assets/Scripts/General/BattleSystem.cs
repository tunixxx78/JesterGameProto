using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BattleState { START, PLAYERTURN, PLAYERTWOTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    //[SerializeField] TMP_Text resultText;
    [SerializeField] GameObject /*playerOne, playerTwo,*/ resultPanelWin, resultPanelLost, playerTurnIndicator;
    //public GameObject[] enemys; //players;
    public List<GameObject> enemys = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();
    public float attackOneDamage, attackTwoDamage;
    public float allPlayerPoints;
    float playersStats;

    Unit playerUnit;
    Unit playerOneUnit;
    Unit playerTwoUnit;

    EnemyUnit enemyOneUnit, enemyTwoUnit;

    EnemyProto enemyProto, enemyTwoProto;

    PlayerMovementGrid playerMovementGrid;

    PlayerMovementGrid playerOnemovement, playerTwoMovement;

    public int enemyCount, playerCount;

    bool battleHasEnded = false, enemyIsAttacking = false;
    public bool attackBoostIsOn = false;
    AttackTile attackTile;

    [SerializeField] Transform playerTurnIndicatorSpawnPoint;
    [SerializeField] float timeToChangeTurn;

   

    

    private void Awake()
    {

        playerMovementGrid = FindObjectOfType<PlayerMovementGrid>();

        //enemyProto = FindObjectOfType<EnemyProto>();
        /*
        for (int i = 0; i < players.Count; i++)
        {
            playersStats = players[i].GetComponent<PlayerMovementGrid>().PlayerPoints;
            Debug.Log(playersStats);

            allPlayerPoints += playersStats;

            var playersDamage = players[i].GetComponent<Unit>().damage;

            attackOneDamage = playersDamage;
        }
        */
        //actionPAmount = FindObjectOfType<ActionPointTile>().extraActionPoints;

        //playerOnemovement = players[0].GetComponent<PlayerMovementGrid>();
        //playerTwoMovement = players[1].GetComponent<PlayerMovementGrid>();

        enemyCount = enemys.Count;
        playerCount = players.Count;

       
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < players.Count; i++)
        {
            playersStats = players[i].GetComponent<PlayerMovementGrid>().PlayerPoints;
            Debug.Log(playersStats);

            allPlayerPoints += playersStats;

            var playersDamage = players[i].GetComponent<Unit>().damage;

            attackOneDamage = playersDamage;
        }
        

        state = BattleState.START;
        SetupBattle();

       
        
    }

    private void Update()
    {
       
        
        if(enemyCount <= 0)
        {
            state = BattleState.WON;
           Invoke("MatchWon", 2f);
        }
        if(playerCount <= 0)
        {
            state = BattleState.LOST;
            MatchLost();
        }
        /*if (!GameObject.FindGameObjectWithTag("Player"))
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
        */
        
        /*if(playerOnemovement.PlayerPoints == 0 && playerTwoMovement.PlayerPoints == 0 && battleHasEnded == false)
        {
            state = BattleState.ENEMYTURN;
            EnemyTurn();
            playerMovementGrid.IsActiveToFalse();
        }


        if (attackBoostIsOn)
        {
            attackOneDamage = playerUnit.damage;
        }
        */
    }

    

    public void AllPlayerPointsCheck()
    { 

        if (allPlayerPoints == 0 && battleHasEnded == false && enemyIsAttacking == false)
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurnIvoke());
            //playerMovementGrid.IsActiveToFalse();
            enemyIsAttacking = true;
        }
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

        PlayerOneTurn();
    }

    public void PlayerOneTurn()
    {
        if(playerCount != 0 && enemyCount != 0)
        {
            state = BattleState.PLAYERTURN;

            GameObject indicator = Instantiate(playerTurnIndicator, playerTurnIndicatorSpawnPoint.position, Quaternion.identity);
            indicator.GetComponentInChildren<TextMeshPro>().text = "Your Turn";

            Destroy(indicator, 7);

            //instructionsText.text = playerOneUnit.unitName;
            if (!GameObject.FindGameObjectWithTag("Player") && !GameObject.FindGameObjectWithTag("Player2"))
            {
                MatchLost();
            }
        }
        
        
    }

    /*public void PlayerTwoTurn()
    {
        state = BattleState.PLAYERTWOTURN;
        //instructionsText.text = playerTwoUnit.unitName;
        if (!GameObject.FindGameObjectWithTag("Player2"))
        {
           EnemyTurn();
        }
    }*/

    public void EnemyTurn()
    {

        //state = BattleState.ENEMYTURN;
        //instructionsText.text = enemyOneUnit.enemyName;
        
        for (int i = 0; i < enemys.Count; i++)
        {
            enemys[i].GetComponent<EnemyProto>().EnemyAction();
            if(enemys[i].GetComponent<EnemySingleShoot>().attackDelay > 0)
            {
                enemys[i].GetComponent<EnemySingleShoot>().attackDelay -= 1;
            }
            else if(enemys[i].GetComponent<EnemySingleShoot>().attackDelay <= 0)
            {
                enemys[i].GetComponent<EnemySingleShoot>().attackDelay = enemys[i].GetComponent<EnemySingleShoot>().startAttackDealy;
            }
            
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
        if (playerCount != 0 && enemyCount != 0)
        {
            yield return new WaitForSeconds(timeToChangeTurn + 1);

            state = BattleState.PLAYERTURN;

            GameObject indicator = Instantiate(playerTurnIndicator, playerTurnIndicatorSpawnPoint.position, Quaternion.identity);
            indicator.GetComponentInChildren<TextMeshPro>().text = "Your turn";

            Destroy(indicator, 7);

            for (int i = 0; i < players.Count; i++)
            {
                players[i].GetComponent<PlayerMovementGrid>().ResetPlayerPoints();

                playersStats = players[i].GetComponent<Unit>().newActionPoints;
                Debug.Log(playersStats);


                allPlayerPoints += playersStats;

                enemyIsAttacking = false;
            }
        }
        
    }

    IEnumerator EnemyTurnIvoke()
    {
        if(enemyCount != 0 && playerCount != 0)
        {
            GameObject indicator = Instantiate(playerTurnIndicator, playerTurnIndicatorSpawnPoint.position, Quaternion.identity);
            indicator.GetComponentInChildren<TextMeshPro>().text = " ENEMY TURN";

            Destroy(indicator, 7);

            yield return new WaitForSeconds(timeToChangeTurn);

            playerMovementGrid.IsActiveToFalse();
            EnemyTurn();
        }
        

        
        
    }
    /*
    public void FindActionPointTileStats()
    {
        //actionPAmount = FindObjectOfType<ActionPointTile>().extraActionPoints;

    }
    */
    IEnumerator Won()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerMovementGrid>().IsActiveToFalse();
        }
        yield return new WaitForSeconds(timeToChangeTurn);

        resultPanelWin.SetActive(true);
        //MoveOnButton.SetActive(true);
        //resultText.text = "You Won This Match!";
        battleHasEnded = true;
    }

    IEnumerator Lost()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerMovementGrid>().IsActiveToFalse();
        }

        yield return new WaitForSeconds(timeToChangeTurn);

        resultPanelLost.SetActive(true);
        //resultText.text = "You Lost This Match!";
        battleHasEnded = true;
    }

    
}
