using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovementGrid : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f, verticalGridSizeMultiplier = 1f, horizontzlGridMultiplier = 0.75f;
    [SerializeField] GameObject pLRPanel;
    public Transform movepoint;
    public LayerMask stopsMovement;
    bool isActive = false;
    [SerializeField] int PlayerPoints, playerStartPoints, pointsForAttack, wantedHP, playerHp;
    [SerializeField] GameObject player;

    PlayerPointManager playerPointManager;

    [SerializeField] TMP_Text playerPointsText, playerName, playerHealtText, playerNameText;

    Unit playerUnit;

    [SerializeField] GameObject ammoPrefab;
    [SerializeField] Transform ammoSpawnPoint;

    BattleSystem battleSystem;
    private void Awake()
    {
        playerUnit = player.GetComponent<Unit>();
        playerStartPoints = playerUnit.playerActionPoints;
        playerPointsText.text = PlayerPoints.ToString();

        wantedHP = playerUnit.maxHP;
        playerHp = wantedHP;

        playerHealtText.text = playerHp.ToString();

        playerName.text = playerUnit.unitName;
        playerNameText.text = playerUnit.unitName;

        battleSystem = FindObjectOfType<BattleSystem>();
    }

    private void Start()
    {
        movepoint.parent = null;
    }

    private void Update()
    {
        PlayerActions();
        playerPointsText.text = PlayerPoints.ToString();
        playerHealtText.text = playerHp.ToString();

        if (playerHp <= 0)
        {
            battleSystem.CountingPlayers();
            Destroy(this.gameObject);
        }
        
    }
    private void OnMouseDown()
    {
        ResetPlayerPoints();
        pLRPanel.SetActive(true);
        
            isActive = true;
        

    }
    public void IsActiveToFalse()
    {
        isActive = false;
    }

    void ResetPlayerPoints()
    {
        PlayerPoints = playerStartPoints;
    }

    private void PlayerActions()
    {
        transform.position = Vector3.MoveTowards(transform.position, movepoint.position, moveSpeed * Time.deltaTime);
        
        if (isActive == true && PlayerPoints >= 1)
            {
            if (Vector3.Distance(transform.position, movepoint.position) <= .05f)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, stopsMovement))
                    {
                        movepoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * horizontzlGridMultiplier, 0f, 0f);
                        PlayerPoints--;
                        

                        isActive = true;
                    }

                }
                if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, stopsMovement))
                    {
                        movepoint.position += new Vector3(0f, (Input.GetAxisRaw("Vertical") * verticalGridSizeMultiplier), 0f);
                        PlayerPoints--;
                        

                        isActive = true;
                    }

                }
            }
        }
        if(isActive == true && PlayerPoints >= pointsForAttack && Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(ammoPrefab, ammoSpawnPoint.position, Quaternion.identity);
            PlayerPoints -= pointsForAttack;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("EnemyBullet"))
        {
            playerHp--;
        }
    }


}
