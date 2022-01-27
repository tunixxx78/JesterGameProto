using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BattleState { START, PLAYERTURN, PLAYERTWOTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    [Header("LEVEL DESIGNER USE!!!")]

    [Tooltip(" Drag all enmys on scene HERE! ")] public List<GameObject> enemys = new List<GameObject>();
    [Tooltip(" Drag all player characters in scene here! ")] public List<GameObject> players = new List<GameObject>();
    
    
    [HideInInspector]
    [Header("PROGRAMER USE!!!")]
    public BattleState state;
    [HideInInspector]
    public int enemyCount, playerCount;
    [HideInInspector]
    [SerializeField] GameObject resultPanelWin, resultPanelLost, playerTurnIndicator;
    [HideInInspector]
    public float attackOneDamage, attackTwoDamage;
    [HideInInspector]
    public float allPlayerPoints;
    [HideInInspector]
    float playersStats;

    Unit playerUnit;
    Unit playerOneUnit;
    Unit playerTwoUnit;

    EnemyUnit enemyOneUnit, enemyTwoUnit;

    EnemyProto enemyProto, enemyTwoProto;

    PlayerMovementGrid playerMovementGrid;

    PlayerMovementGrid playerOnemovement, playerTwoMovement;

    

    bool battleHasEnded = false, enemyIsAttacking = false;
    [HideInInspector]
    public bool attackBoostIsOn = false;
    AttackTile attackTile;

    [HideInInspector]
    [SerializeField] Transform playerTurnIndicatorSpawnPoint;
    [HideInInspector]
    [SerializeField] float timeToChangeTurn;

   

    

    private void Awake()
    {

        playerMovementGrid = FindObjectOfType<PlayerMovementGrid>();

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

            if (!GameObject.FindGameObjectWithTag("Player") && !GameObject.FindGameObjectWithTag("Player2"))
            {
                MatchLost();
            }
        }
        
        
    }


    public void EnemyTurn()
    {
        
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
    
    IEnumerator Won()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerMovementGrid>().IsActiveToFalse();
        }
        yield return new WaitForSeconds(timeToChangeTurn);

        resultPanelWin.SetActive(true);

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
        battleHasEnded = true;
    }

    
}
