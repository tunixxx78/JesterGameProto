using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovementGrid : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f, verticalGridSizeMultiplier = 1f, horizontzlGridMultiplier = 0.75f, ammoRange, timeToShowSpecialTileEffect = 2f, indicatorReSpawnTime = 1, timeToSetAttackinToFalse = 2, timeToSetPlayerIsMovingToFalse = 1;
    [SerializeField] GameObject pLRPanel, selectedPlayerIcon, seeker_AttackFX, attackRangeIndicator, specialTileEffectPrefab, movingRangeIndicator;
    public Transform movepoint;
    public LayerMask stopsMovement, enemyMask, playerMask;
    public bool isActive = false, firstClickDone = false, indicatorCanMove = false, playerIsAttacking = false, playerIsMoving = false, movingIndicatorIsOn = false;
    public int PlayerPoints, playerStartPoints, pointsForAttack, wantedHP, playerHp;
    [SerializeField] int enemySingleShotDamage;
    //[SerializeField] GameObject player, player2, enemyOne;

    public List<GameObject> playeers = new List<GameObject>();
    public List<GameObject> impactIndicators = new List<GameObject>();

    PlayerPointManager playerPointManager;

    [SerializeField] TMP_Text playerPointsText, playerAttackCostText, SpecialTileEffectText;

    Unit playerUnit;

    SFXManager sFXManager;

    SingleTargetAttack singleTargetAttack;

    [SerializeField] GameObject ammoPrefab;
    [SerializeField] Transform ammoSpawnPoint, movingIndicatorSpawnPoint;
    public Transform bulletTargetRange;

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
        playerUnit = GetComponent<Unit>();

        playerStartPoints = playerUnit.playerActionPoints;
        PlayerPoints = playerStartPoints;

        playerPointsText.text = PlayerPoints.ToString();
        battleSystem = FindObjectOfType<BattleSystem>();
        
        
        
        wantedHP = playerUnit.maxHP;
        playerHp = wantedHP;

        playerAttackCostText.text = pointsForAttack.ToString();

        singleTargetAttack = FindObjectOfType<SingleTargetAttack>();

        
        ammoRange = singleTargetAttack.bulletRange / 2;

        if (FindObjectOfType<EnemySingleShoot>().enabled)
        {
            enemySingleShotDamage = FindObjectOfType<EnemySingleShoot>().bulletDamage;
        }
        

        sFXManager = FindObjectOfType<SFXManager>();

        
    }

    private void Start()
    {
        movepoint.parent = null;

        healthBar.SetMaxValue(wantedHP);

        for(int i = 0; i < impactIndicators.Count; i++)
        {

        }

        bulletTargetRange.transform.position = bulletTargetRange.transform.position + new Vector3(0, ammoRange, 0);

    }

    private void Update()
    {
        PlayerActions();

        Swipe();  // related to swipe controls

        ammoRange = singleTargetAttack.bulletRange / 2;

        

        playerPointsText.text = PlayerPoints.ToString();

        if (playerHp <= 0)
        {
            battleSystem.CountingPlayers();

            battleSystem.players.Remove(this.gameObject);

            Destroy(this.gameObject);
        }

        if(isActive == false)
        {
            //teamPoints = 0;
            selectedPlayerIcon.SetActive(false);
        }
        //if(isActive == true && battleSystem.enemyCount != 0 && player.GetComponent<PlayerMovementGrid>().isActive == true && player2.GetComponent<PlayerMovementGrid>().isActive == false)

        /*for(int i = 0; i < playeers.Count; i++)
        {
            this.GetComponent<PlayerMovementGrid>();

            if (isActive == true && battleSystem.enemyCount != 0 && playeers[i].GetComponent<PlayerMovementGrid>().isActive == true)
            {
                for (int e = 0; e < battleSystem.enemys.Count; e++)
                {
                    //EnemyProto notSellectedEnemy = enemyOne.GetComponent<EnemyProto>();
                    var notSellectedEnemy = battleSystem.enemys[e].GetComponent<EnemyProto>();
                    notSellectedEnemy.inTargetIcon.SetActive(false);

                    RaycastHit2D hitInfo = Physics2D.Raycast(movingIndicatorSpawnPoint.position, ammoSpawnPoint.up, enemyMask);
                    EnemyProto enemy = hitInfo.transform.GetComponent<EnemyProto>();

                    Debug.Log(hitInfo);

                    if (enemy != null)
                    {
                        enemy.inTargetIcon.SetActive(true);
                    }
                }


            }
        }*/

        if (Input.GetKeyDown(KeyCode.P))
        {
            IsActiveToFalse();
        }
        
    }
    private void LateUpdate()
    {
        playerPointsText.text = PlayerPoints.ToString();
    }

    // player movement controls with swipe system

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
        if(this.isActive == false)
        {
            for (int i = 0; i < playeers.Count; i++)
            {
                this.GetComponent<PlayerMovementGrid>();
                this.IsActiveToFalse();
                //StartCoroutine(SellectPlayer());
                //ResetPlayerPoints();
                sFXManager.button.Play();

                this.StartCoroutine(SellectNewPlayer());
            }
        }

        
        
        
        
    }
    IEnumerator SellectNewPlayer()
    {
        yield return new WaitForSeconds(.2f);

        pLRPanel.SetActive(true);
        selectedPlayerIcon.SetActive(true);

        isActive = true;
    }

    public void EndCurrentTurn()
    {
        if(playerIsMoving == false && playerIsAttacking == false)
        {
            for (int i = 0; i < playeers.Count; i++)
            {
                this.GetComponent<PlayerMovementGrid>();

                battleSystem.allPlayerPoints -= this.PlayerPoints;
                this.PlayerPoints = 0;

                firstClickDone = false;
                attackRangeIndicator.SetActive(false);

                this.indicatorCanMove = false;

                playerPointsText.text = PlayerPoints.ToString();
                sFXManager.button.Play();
            }
        }
        
        
        
        
    }


    public void IsActiveToFalse()
    {

        for(int i = 0; i < playeers.Count; i++)
        {
            //players[i].GetComponent<PlayerMovementGrid>();
            playeers[i].GetComponent<PlayerMovementGrid>().isActive = false;
            playeers[i].GetComponent<PlayerMovementGrid>().selectedPlayerIcon.SetActive(false);
            playeers[i].GetComponent<PlayerMovementGrid>().pLRPanel.SetActive(false);
            playeers[i].GetComponent<PlayerMovementGrid>().attackRangeIndicator.SetActive(false);
        }
        
    }

    public void ResetPlayerPoints()
    {
        //GetComponent<Unit>().newActionPoints += 1;
        PlayerPoints = GetComponent<Unit>().newActionPoints;
    }

    // player movements functionality with swipe system
    private void PlayerMoveLeft()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, movepoint.position, moveSpeed * Time.deltaTime);

        if (isActive == true && PlayerPoints >= 1 && playerIsAttacking == false)
        {
            
            if (Vector3.Distance(transform.position, movepoint.position) <= .05f)
            {
                
                if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(-1f, 0f, 0f), .2f, stopsMovement))
                {
                    var hit = Physics2D.CircleCast(movepoint.position + new Vector3(-1f, 0f, 0f), 0.1f, Vector3.zero, Mathf.Infinity, (playerMask));

                    if (!hit.collider)
                    {
                        Debug.Log("T????L???? OLLAAAAAN");
                        movepoint.position += new Vector3(-1f * horizontzlGridMultiplier, 0f, 0f);
                        playerAnimator.SetBool("isWalking", true);
                        PlayerPoints--;
                        battleSystem.allPlayerPoints--;
                        sFXManager.playerMoving.Play();

                        firstClickDone = false;
                        attackRangeIndicator.SetActive(false);
                        indicatorCanMove = false;

                        StartCoroutine(KillWalkingAnimation());

                        playerIsMoving = true;
                        StartCoroutine(SetPlayerIsMovingToFalse());
                    }
                    
                }
            }
        }
        //playerAnimator.SetBool("isWalking", false);
    }

    // player movements functionality with swipe system
    private void PlayerMoveRight()
    {
        transform.position = Vector3.MoveTowards(transform.position, movepoint.position, moveSpeed * Time.deltaTime);

        if (isActive == true && PlayerPoints >= 1 && playerIsAttacking == false)
        {

            if (Vector3.Distance(transform.position, movepoint.position) <= .05f)
            {

                if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(1f, 0f, 0f), .2f, stopsMovement))
                {
                    var hit = Physics2D.CircleCast(movepoint.position + new Vector3(1f, 0f, 0f), 0.1f, Vector3.zero, Mathf.Infinity, (playerMask));

                    if (!hit.collider)
                    {
                        Debug.Log("T????L???? OLLAAAAAN");
                        movepoint.position += new Vector3(1f * horizontzlGridMultiplier, 0f, 0f);
                        playerAnimator.SetBool("isWalking", true);
                        PlayerPoints--;
                        battleSystem.allPlayerPoints--;
                        sFXManager.playerMoving.Play();
                        isActive = true;

                        firstClickDone = false;
                        attackRangeIndicator.SetActive(false);
                        indicatorCanMove = false;

                        StartCoroutine(KillWalkingAnimation());

                        playerIsMoving = true;
                        StartCoroutine(SetPlayerIsMovingToFalse());
                    }
                    
                }
            }
        }
    }

    // player movements functionality with swipe system
    private void PlayerMoveUp()
    {
        transform.position = Vector3.MoveTowards(transform.position, movepoint.position, moveSpeed * Time.deltaTime);

        if (isActive == true && PlayerPoints >= 1 && transform.position.y <= -1 && playerIsAttacking == false)
        {

            if (Vector3.Distance(transform.position, movepoint.position) <= .05f)
            {

                if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(0f, 1f, 0f), .2f, stopsMovement))
                {
                    var hit = Physics2D.CircleCast(movepoint.position + new Vector3(0f, 1f, 0f), 0.0001f, Vector3.zero, Mathf.Infinity, (playerMask));

                    if (!hit.collider)
                    {
                        Debug.Log("T????L???? OLLAAAAAN");
                        movepoint.position += new Vector3(0f, 1f * verticalGridSizeMultiplier, 0f);
                        playerAnimator.SetBool("isWalking", true);
                        PlayerPoints--;
                        battleSystem.allPlayerPoints--;
                        sFXManager.playerMoving.Play();
                        isActive = true;

                        firstClickDone = false;
                        attackRangeIndicator.SetActive(false);
                        indicatorCanMove = false;

                        StartCoroutine(KillWalkingAnimation());

                        playerIsMoving = true;
                        StartCoroutine(SetPlayerIsMovingToFalse());
                    }
                    
                }
            }
        }
    }

    // player movements functionality with swipe system
    private void PlayerMoveDown()
    {
        transform.position = Vector3.MoveTowards(transform.position, movepoint.position, moveSpeed * Time.deltaTime);

        if (isActive == true && PlayerPoints >= 1 && playerIsAttacking == false)
        {

            if (Vector3.Distance(transform.position, movepoint.position) <= .05f)
            {

                if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(0f, -1f, 0f), .2f, stopsMovement))
                {
                    var hit = Physics2D.CircleCast(movepoint.position + new Vector3(0f, -1f, 0f), 0.0001f, Vector3.zero, Mathf.Infinity, (playerMask));

                    if (!hit.collider)
                    {
                        Debug.Log("T????L???? OLLAAAAAN");
                        movepoint.position += new Vector3(0f, -1f * verticalGridSizeMultiplier, 0f);
                        playerAnimator.SetBool("isWalking", true);
                        PlayerPoints--;
                        battleSystem.allPlayerPoints--;
                        sFXManager.playerMoving.Play();
                        isActive = true;

                        firstClickDone = false;
                        attackRangeIndicator.SetActive(false);
                        indicatorCanMove = false;

                        StartCoroutine(KillWalkingAnimation());

                        playerIsMoving = true;
                        StartCoroutine(SetPlayerIsMovingToFalse());
                    }
                    
                }
            }
        }
    }

    //Player movements with keyboard commands

    private void PlayerActions()
    {   
        transform.position = Vector3.MoveTowards(transform.position, movepoint.position, moveSpeed * Time.deltaTime);

        if (isActive == true && PlayerPoints >= 1 && playerIsAttacking == false)
        {
            if (Vector3.Distance(transform.position, movepoint.position) <= .05f)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, stopsMovement))
                    {
                        var hit = Physics2D.CircleCast(movepoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), 0.1f, Vector3.zero, Mathf.Infinity, (playerMask));

                        if (!hit.collider)
                        {
                            movepoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * horizontzlGridMultiplier, 0f, 0f);
                            PlayerPoints--;
                            battleSystem.allPlayerPoints--;
                            sFXManager.playerMoving.Play();

                            isActive = true;

                            firstClickDone = false;
                            attackRangeIndicator.SetActive(false);
                            indicatorCanMove = false;

                            playerIsMoving = true;
                            StartCoroutine(SetPlayerIsMovingToFalse());
                        }
                        
                    }

                }
                if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, stopsMovement))
                    {
                        var hit = Physics2D.CircleCast(movepoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), 0.00011f, Vector3.zero, Mathf.Infinity, (playerMask));

                        if (!hit.collider)
                        {
                            
                            movepoint.position += new Vector3(0f, (Input.GetAxisRaw("Vertical") * verticalGridSizeMultiplier), 0f);
                            PlayerPoints--;
                            battleSystem.allPlayerPoints--;
                            sFXManager.playerMoving.Play();

                            isActive = true;

                            firstClickDone = false;
                            attackRangeIndicator.SetActive(false);
                            indicatorCanMove = false;

                            playerIsMoving = true;
                            StartCoroutine(SetPlayerIsMovingToFalse());
                        }
                        
                    }

                }
            }
        }

        if (isActive == true && PlayerPoints >= pointsForAttack && Input.GetKeyDown(KeyCode.Space))
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

            PlayerPoints -= pointsForAttack;
            battleSystem.allPlayerPoints -= pointsForAttack;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "AttackBoost")
        {
            
            playerUnit.InCreaseAttackPower();

            battleSystem.attackBoostIsOn = false;

            battleSystem.attackOneDamage = battleSystem.attackOneDamage + FindObjectOfType<AttackTile>().damageMultiplier;

            // For showing player special tiles effect to damage.
            GameObject tileEffectPrefab = Instantiate(specialTileEffectPrefab, ammoSpawnPoint.position, Quaternion.identity);
            tileEffectPrefab.GetComponent<TextMeshPro>().text = "+" + FindObjectOfType<AttackTile>().damageMultiplier.ToString();
            
            // for hiding above
            Destroy(tileEffectPrefab, timeToShowSpecialTileEffect);
            

            sFXManager.attackTile.Play();
        }
        if(collision.gameObject.tag == "EnemyBullet")
        {
            PlayerTakeDamage(enemySingleShotDamage);
        }
        if(collision.gameObject.tag == "DefenceTile")
        {
            enemySingleShotDamage = enemySingleShotDamage / FindObjectOfType<DefenceTile>().armourAmount;

            // For showing player special tiles effect to damage.
            GameObject tileEffectPrefab = Instantiate(specialTileEffectPrefab, ammoSpawnPoint.position, Quaternion.identity);
            tileEffectPrefab.GetComponent<TextMeshPro>().text = "/" + FindObjectOfType<DefenceTile>().armourAmount.ToString();

            // for hiding above
            Destroy(tileEffectPrefab, timeToShowSpecialTileEffect);

            sFXManager.attackTile.Play();
        }
        if(collision.gameObject.tag == "RangeTile")
        {
            if(battleSystem.rangeIsNegative == false)
            {
                singleTargetAttack.bulletRange = singleTargetAttack.bulletRange + battleSystem.rangeTileAmount;
                //float extraRange = FindObjectOfType<RangeTile>().rangeAmount;
                //Debug.Log(extraRange);
                bulletTargetRange.transform.position = bulletTargetRange.transform.position + new Vector3(0, battleSystem.rangeTileAmount / 2f, 0); 

                // For showing player special tiles effect to damage.
                GameObject tileEffectPrefab = Instantiate(specialTileEffectPrefab, ammoSpawnPoint.position, Quaternion.identity);
                tileEffectPrefab.GetComponentInChildren<TextMeshPro>().text = "+" + battleSystem.rangeTileAmount;

                // for hiding above
                Destroy(tileEffectPrefab, timeToShowSpecialTileEffect);

                sFXManager.attackTile.Play();
            }
            if (battleSystem.rangeIsNegative == true)
            {
                singleTargetAttack.bulletRange = singleTargetAttack.bulletRange - battleSystem.rangeTileAmount;
                //float extraRange = FindObjectOfType<RangeTile>().rangeAmount;
                //Debug.Log(extraRange);
                bulletTargetRange.transform.position = bulletTargetRange.transform.position - new Vector3(0, battleSystem.rangeTileAmount / 2f, 0); 

                // For showing player special tiles effect to damage.
                GameObject tileEffectPrefab = Instantiate(specialTileEffectPrefab, ammoSpawnPoint.position, Quaternion.identity);
                tileEffectPrefab.GetComponentInChildren<TextMeshPro>().text = "-" + battleSystem.rangeTileAmount;

                // for hiding above
                Destroy(tileEffectPrefab, timeToShowSpecialTileEffect);

                sFXManager.attackTile.Play();
            }
            
        }
        if(collision.gameObject.tag == "ActionPointTile")
        {
            PlayerPoints = PlayerPoints + FindObjectOfType<ActionPointTile>().extraActionPoints;
            battleSystem.allPlayerPoints = battleSystem.allPlayerPoints + FindObjectOfType<ActionPointTile>().extraActionPoints;

            // For showing player special tiles effect to damage.
            GameObject tileEffectPrefab = Instantiate(specialTileEffectPrefab, ammoSpawnPoint.position, Quaternion.identity);
            tileEffectPrefab.GetComponent<TextMeshPro>().text = "+" + FindObjectOfType<ActionPointTile>().extraActionPoints.ToString();

            // for hiding above
            Destroy(tileEffectPrefab, timeToShowSpecialTileEffect);

            sFXManager.attackTile.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AttackBoost")
        {
            playerUnit.DeCreaseAttackPower();
            battleSystem.attackBoostIsOn = true;

            // For showing player special tiles effect to damage.
            GameObject tileEffectPrefab = Instantiate(specialTileEffectPrefab, ammoSpawnPoint.position, Quaternion.identity);
            tileEffectPrefab.GetComponent<TextMeshPro>().text = "-" + FindObjectOfType<AttackTile>().damageMultiplier.ToString();

            // for hiding above
            Destroy(tileEffectPrefab, timeToShowSpecialTileEffect);

        }
        if (collision.gameObject.tag == "DefenceTile")
        {
            enemySingleShotDamage = FindObjectOfType<EnemySingleShoot>().bulletDamage;
        }
        if (collision.gameObject.tag == "RangeTile")
        {
            if(battleSystem.rangeIsNegative == false)
            {
                singleTargetAttack.bulletRange = FindObjectOfType<RangeTile>().starBulletRange;
                //float extraRange = FindObjectOfType<RangeTile>().rangeAmount;
                //Debug.Log(extraRange);
                bulletTargetRange.transform.position = bulletTargetRange.transform.position - new Vector3(0, battleSystem.rangeTileAmount / 2, 0);

                // For showing player special tiles effect to damage.
                GameObject tileEffectPrefab = Instantiate(specialTileEffectPrefab, ammoSpawnPoint.position, Quaternion.identity);
                tileEffectPrefab.GetComponentInChildren<TextMeshPro>().text = "-" + battleSystem.rangeTileAmount;

                // for hiding above
                Destroy(tileEffectPrefab, timeToShowSpecialTileEffect);
            }
            if(battleSystem.rangeIsNegative == true)
            {
                singleTargetAttack.bulletRange = FindObjectOfType<RangeTile>().starBulletRange;
                //float extraRange = FindObjectOfType<RangeTile>().rangeAmount;
                //Debug.Log(extraRange);
                bulletTargetRange.transform.position = bulletTargetRange.transform.position + new Vector3(0, battleSystem.rangeTileAmount / 2, 0);

                // For showing player special tiles effect to damage.
                GameObject tileEffectPrefab = Instantiate(specialTileEffectPrefab, ammoSpawnPoint.position, Quaternion.identity);
                tileEffectPrefab.GetComponentInChildren<TextMeshPro>().text = "+" + battleSystem.rangeTileAmount;

                // for hiding above
                Destroy(tileEffectPrefab, timeToShowSpecialTileEffect);
            }
            
        }
    }

    // Player attack button doubleClick funktionality.

    public void PlayerAttackPositions()
    {
        // for (int i = 0; i < playeers.Count; i++)
        // {

       if(movingIndicatorIsOn == false && playerIsAttacking == false)
        {
            movingIndicatorIsOn = true;

            this.GetComponent<PlayerMovementGrid>();
            this.attackRangeIndicator.SetActive(true);
            this.indicatorCanMove = true;

            this.StartCoroutine(MovingIIndicators());

            GameObject movingIndicator = Instantiate(movingRangeIndicator, movingIndicatorSpawnPoint.position, Quaternion.identity);

            if (this.firstClickDone == false)
            {
                this.firstClickDone = true;
            }
            //this.GetComponent<PlayerMovementGrid>();
            //this.IsActiveToFalse();
            //StartCoroutine(SellectPlayer());
            //ResetPlayerPoints();
            //sFXManager.button.Play();

            //this.StartCoroutine(SellectNewPlayer());
            //}
        }
       else if (movingIndicatorIsOn && playerIsAttacking == false)
        {
            if (this.firstClickDone)
            {
                PlayerStartAttack();
                indicatorCanMove = false;
                firstClickDone = false;
                attackRangeIndicator.SetActive(false);
                movingIndicatorIsOn = false;

            }
        }
    }

    private void SpawnIndicator()
    {

        GameObject movingIndicator = Instantiate(movingRangeIndicator, movingIndicatorSpawnPoint.position, Quaternion.identity);
            
    }

    IEnumerator MovingIIndicators()
    {

    while (indicatorCanMove == true)
    {
        yield return new WaitForSeconds(indicatorReSpawnTime);

        this.SpawnIndicator();
    }
            
    }

    // Player attack itself

    public void PlayerStartAttack()
    {
        playerIsAttacking = true;

        for (int i = 0; i < playeers.Count; i++)
        {
            if (isActive == true && PlayerPoints >= pointsForAttack)
            {
                
                playerAnimator.SetTrigger("isShooting");
                sFXManager.playerPreShoot.Play();

            }
        }
        

    }

    public void StopAttacking()
    {
        playerAnimator.SetBool("isShooting", false);
    }

    // Shooting / ability functionalitys

    public void PlayerOneAttack()
    {
        for (int i = 0; i < playeers.Count; i++)
        {
            if(this.playeers[i].GetComponent<PlayerMovementGrid>().isActive == true)
            {
                if (/*isActive == true && */PlayerPoints >= pointsForAttack)
                {
                    RaycastHit2D hitInfo = Physics2D.Raycast(ammoSpawnPoint.position, ammoSpawnPoint.up, enemyMask);



                    if (hitInfo)
                    {
                        EnemyProto enemy = hitInfo.transform.GetComponent<EnemyProto>();
                        Debug.Log(hitInfo.transform.name);

                        if (enemy != null)
                        {
                            enemy.inTargetIcon.SetActive(true);
                            //enemy.TakeDamage(playerUnit.damage);
                        }
                    }
                    this.playeers[i].GetComponent<SingleTargetAttack>().PlayerSingleTargetAttack();
                    PlayerPoints -= pointsForAttack;
                    battleSystem.allPlayerPoints -= pointsForAttack;

                    StartCoroutine(SetAttackingToFalse());

                }
            }
            
        }
        
        
        
    }

    public void PlayerTwoAttack()
    {
        if (isActive == true && PlayerPoints >= pointsForAttack)
        {
            /*RaycastHit2D hitInfo = Physics2D.Raycast(ammoSpawnPoint.position, ammoSpawnPoint.up, 10f, enemyMask);



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
            */
            //player2.GetComponent<AOEAttack>().PlayerAOEAttack();

            PlayerPoints -= pointsForAttack;
            battleSystem.allPlayerPoints -= pointsForAttack;


        }
    
    }

    IEnumerator KillWalkingAnimation()
    {
        yield return new WaitForSeconds(.5f);

        playerAnimator.SetBool("isWalking", false);
    }
    
    IEnumerator SetAttackingToFalse()
    {
        yield return new WaitForSeconds(timeToSetAttackinToFalse);

        playerIsAttacking = false;
    }
    IEnumerator SetPlayerIsMovingToFalse()
    {
        yield return new WaitForSeconds(timeToSetPlayerIsMovingToFalse);
        playerIsMoving = false;
    }


}
