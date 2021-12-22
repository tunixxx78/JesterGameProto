using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovementGrid : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f, verticalGridSizeMultiplier = 1f, horizontzlGridMultiplier = 0.75f;
    [SerializeField] GameObject pLRPanel, selectedPlayerIcon, seeker_AttackFX;
    public Transform movepoint;
    public LayerMask stopsMovement, enemyMask;
    bool isActive = false;
    [SerializeField] int PlayerPoints, playerStartPoints, pointsForAttack, wantedHP, playerHp;
    [SerializeField] GameObject player, player2, enemyOne;

    PlayerPointManager playerPointManager;

    [SerializeField] TMP_Text playerPointsText, playerName, playerHealtText, playerNameText;

    Unit playerUnit;

    [SerializeField] GameObject ammoPrefab;
    [SerializeField] Transform ammoSpawnPoint;

    BattleSystem battleSystem;

    [SerializeField] HealthBar healthBar;

    EnemyProto enemyProto;

    [SerializeField] Animator playerAnimator;

    // These are for mobile swipe control system

    Vector2 startTouchPosition, currentPosition, endTouchPosition;
    bool stopTouch = false;
    public float swipeRange, tapRange;

    private void Awake()
    {
        playerUnit = player.GetComponent<Unit>();
        //playerStartPoints = playerUnit.playerActionPoints;
        playerPointsText.text = PlayerPoints.ToString();
        battleSystem = FindObjectOfType<BattleSystem>();
        
        
        
        wantedHP = playerUnit.maxHP;
        playerHp = wantedHP;

        //playerHealtText.text = playerHp.ToString();

        playerName.text = playerUnit.unitName;
        //playerNameText.text = playerUnit.unitName;

        

    }

    private void Start()
    {
        movepoint.parent = null;

        healthBar.SetMaxValue(wantedHP);

    }

    private void Update()
    {
        PlayerActions();

        Swipe();  // related to swipe controls

        //playerPointsText.text = PlayerPoints.ToString();
        playerHealtText.text = playerHp.ToString();

        if (playerHp <= 0)
        {
            battleSystem.CountingPlayers();
            Destroy(this.gameObject);
        }

        if(isActive == false)
        {
            //teamPoints = 0;
            selectedPlayerIcon.SetActive(false);
        }
        if(isActive == true)
        {
            EnemyProto notSellectedEnemy = enemyOne.GetComponent<EnemyProto>();
            notSellectedEnemy.inTargetIcon.SetActive(false);

            RaycastHit2D hitInfo = Physics2D.Raycast(ammoSpawnPoint.position, ammoSpawnPoint.up, enemyMask);
            EnemyProto enemy = hitInfo.transform.GetComponent<EnemyProto>();

            if (enemy != null)
            {
                enemy.inTargetIcon.SetActive(true);
            }
            
        }
        
    }

    public void Swipe()
    {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentPosition = Input.GetTouch(0).position;
            Vector2 distance = currentPosition - startTouchPosition;

            if(!stopTouch)
            {
                if(distance.x < -swipeRange)
                {
                    PlayerMoveLeft();
                    Debug.Log("Left");
                    stopTouch = true;
                }
                else if (distance.x > swipeRange)
                {
                    PlayerMoveRight();
                    Debug.Log("Right");
                    stopTouch = true;
                }
                else if (distance.y > swipeRange)
                {
                    PlayerMoveUp();
                    Debug.Log("Up");
                    stopTouch = true;
                }
                else if (distance.y < -swipeRange)
                {
                    PlayerMoveDown();
                    Debug.Log("Down");
                    stopTouch = true;
                }
            }

        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;

            endTouchPosition = Input.GetTouch(0).position;

            Vector2 distance = endTouchPosition - startTouchPosition;

            
            
            /*if(Mathf.Abs(distance.x) < tapRange && Mathf.Abs(distance.y) < tapRange)
            {
                Debug.Log("TAPPED!");
                IsActiveToFalse();
                ResetPlayerPoints();
                pLRPanel.SetActive(true);
                selectedPlayerIcon.SetActive(true);

                isActive = true;
            }*/
        }
    }

    private void OnMouseDown()
    {
        IsActiveToFalse();
        //StartCoroutine(SellectPlayer());
        ResetPlayerPoints();
        pLRPanel.SetActive(true);
        selectedPlayerIcon.SetActive(true);
        
        isActive = true;

        
        
        
    }
    public void IsActiveToFalse()
    {
        

        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            var p2 = player2.GetComponent<PlayerMovementGrid>();
            p2.selectedPlayerIcon.SetActive(false);
            p2.pLRPanel.SetActive(false);
            p2.isActive = false;
            
        }
        if (!GameObject.FindGameObjectWithTag("Player2"))
        {
            var p1 = player.GetComponent<PlayerMovementGrid>();
            p1.selectedPlayerIcon.SetActive(false);
            p1.pLRPanel.SetActive(false);
            p1.isActive = false;
            
        }
        else
        {
            var p1_1 = player.GetComponent<PlayerMovementGrid>();
            var p2_2 = player2.GetComponent<PlayerMovementGrid>();
            p1_1.selectedPlayerIcon.SetActive(false);
            p2_2.selectedPlayerIcon.SetActive(false);
            p1_1.pLRPanel.SetActive(false);
            p2_2.pLRPanel.SetActive(false);

            p1_1.isActive = false;
            p2_2.isActive = false;

            
        }
        
    }

    void ResetPlayerPoints()
    {
        
    }

    private void PlayerMoveLeft()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, movepoint.position, moveSpeed * Time.deltaTime);

        if (isActive == true && battleSystem.TeamActionPoints >= 1)
        {
            
            if (Vector3.Distance(transform.position, movepoint.position) <= .05f)
            {
                
                if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(-1f, 0f, 0f), .2f, stopsMovement))
                {
                    Debug.Log("T????L???? OLLAAAAAN");
                    movepoint.position += new Vector3(-1f * horizontzlGridMultiplier, 0f, 0f);
                    playerAnimator.SetBool("isWalking", true);
                    battleSystem.TeamActionPoints--;
                    isActive = true;
                    StartCoroutine(KillWalkingAnimation());
                }
            }
        }
        //playerAnimator.SetBool("isWalking", false);
    }

    private void PlayerMoveRight()
    {
        transform.position = Vector3.MoveTowards(transform.position, movepoint.position, moveSpeed * Time.deltaTime);

        if (isActive == true && battleSystem.TeamActionPoints >= 1)
        {

            if (Vector3.Distance(transform.position, movepoint.position) <= .05f)
            {

                if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(1f, 0f, 0f), .2f, stopsMovement))
                {
                    Debug.Log("T????L???? OLLAAAAAN");
                    movepoint.position += new Vector3(1f * horizontzlGridMultiplier, 0f, 0f);
                    battleSystem.TeamActionPoints--;
                    isActive = true;
                }
            }
        }
    }

    private void PlayerMoveUp()
    {
        transform.position = Vector3.MoveTowards(transform.position, movepoint.position, moveSpeed * Time.deltaTime);

        if (isActive == true && battleSystem.TeamActionPoints >= 1)
        {

            if (Vector3.Distance(transform.position, movepoint.position) <= .05f)
            {

                if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(0f, 1f, 0f), .2f, stopsMovement))
                {
                    Debug.Log("T????L???? OLLAAAAAN");
                    movepoint.position += new Vector3(0f, 1f * verticalGridSizeMultiplier, 0f);
                    battleSystem.TeamActionPoints--;
                    isActive = true;
                }
            }
        }
    }

    private void PlayerMoveDown()
    {
        transform.position = Vector3.MoveTowards(transform.position, movepoint.position, moveSpeed * Time.deltaTime);

        if (isActive == true && battleSystem.TeamActionPoints >= 1)
        {

            if (Vector3.Distance(transform.position, movepoint.position) <= .05f)
            {

                if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(0f, -1f, 0f), .2f, stopsMovement))
                {
                    Debug.Log("T????L???? OLLAAAAAN");
                    movepoint.position += new Vector3(0f, -1f * verticalGridSizeMultiplier, 0f);
                    battleSystem.TeamActionPoints--;
                    isActive = true;
                }
            }
        }
    }

    private void PlayerActions()
    {   
        transform.position = Vector3.MoveTowards(transform.position, movepoint.position, moveSpeed * Time.deltaTime);

        if (isActive == true && battleSystem.TeamActionPoints >= 1)
        {
            if (Vector3.Distance(transform.position, movepoint.position) <= .05f)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, stopsMovement))
                    {
                        movepoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * horizontzlGridMultiplier, 0f, 0f);
                        battleSystem.TeamActionPoints--;


                        isActive = true;
                    }

                }
                if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, stopsMovement))
                    {
                        movepoint.position += new Vector3(0f, (Input.GetAxisRaw("Vertical") * verticalGridSizeMultiplier), 0f);
                        battleSystem.TeamActionPoints--;


                        isActive = true;
                    }

                }
            }
        }
        if (isActive == true && battleSystem.TeamActionPoints >= pointsForAttack && Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(ammoSpawnPoint.position, ammoSpawnPoint.up, enemyMask);
            if (hitInfo)
            {
                Debug.Log(hitInfo.transform.name);
                EnemyProto enemy = hitInfo.transform.GetComponent<EnemyProto>();
                if(enemy != null)
                {
                    
                    enemy.TakeDamage(playerUnit.damage);
                }
            }
            GameObject shootingParticles =  Instantiate(seeker_AttackFX, ammoSpawnPoint.position, Quaternion.identity);
            Destroy(shootingParticles, 1f);

            battleSystem.TeamActionPoints -= pointsForAttack;
        }

    }

    public void PlayerTakeDamage(int damage)
    {
        playerHp -= damage;
        healthBar.SetHealth(playerHp);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("EnemyBullet"))
        {
            playerHp--;
            healthBar.SetHealth(playerHp);
        }
    }

    public void PlayerStartAttack()
    {
        playerAnimator.SetBool("isShooting", true);
    }

    public void StopAttacking()
    {
        playerAnimator.SetBool("isShooting", false);
    }

    public void PlayerOneAttack()
    {

        if (isActive == true && battleSystem.TeamActionPoints >= pointsForAttack)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(ammoSpawnPoint.position, ammoSpawnPoint.up, enemyMask);
            
            

            if (hitInfo)
            {
                EnemyProto enemy = hitInfo.transform.GetComponent<EnemyProto>();
                Debug.Log(hitInfo.transform.name);
                
                if (enemy != null)
                {
                    enemy.inTargetIcon.SetActive(true);
                    enemy.TakeDamage(playerUnit.damage);
                }
            }
            GameObject shootingParticles = Instantiate(seeker_AttackFX, ammoSpawnPoint.position, Quaternion.identity);
            Destroy(shootingParticles, 1f);

            battleSystem.TeamActionPoints -= pointsForAttack;

            
        }
        
        
    }
    IEnumerator KillWalkingAnimation()
    {
        yield return new WaitForSeconds(.5f);

        playerAnimator.SetBool("isWalking", false);
    }
    


}
