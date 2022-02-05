using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovementGrid : MonoBehaviour
{
    [Header("LEVEL DESIGNER USE!!!")]

    [Tooltip("Add all your player characters Here! Element 0 needs to be other than this character!!!")]
    public List<GameObject> playeers = new List<GameObject>();


    [HideInInspector]
    [Header ("FOR PROGRAMMER USE!!!")]
    [SerializeField] Sprite[] iconSprites;
    [HideInInspector]
    [SerializeField] GameObject pLRPanel;
    [HideInInspector]
    [SerializeField] GameObject selectedPlayerIcon;
    [HideInInspector]
    [SerializeField] GameObject seeker_AttackFX;
    [HideInInspector]
    [SerializeField] GameObject attackRangeIndicator;
    [HideInInspector]
    [SerializeField] GameObject specialTileEffectPrefab;
    [HideInInspector]
    [SerializeField] GameObject movingRangeIndicator;
    [HideInInspector]
    [SerializeField] float verticalGridSizeMultiplier = 1f;
    [HideInInspector]
    [SerializeField] float horizontzlGridMultiplier = 0.75f;
    [HideInInspector]
    [SerializeField] float moveSpeed = 5f;
    [HideInInspector]
    [SerializeField] float ammoRange;
    [HideInInspector]
    [SerializeField] float timeToShowSpecialTileEffect = 2f;
    [HideInInspector]
    [SerializeField] float indicatorReSpawnTime = 1;
    [HideInInspector]
    [SerializeField] float timeToSetAttackinToFalse = 2;
    [HideInInspector]
    [SerializeField] float timeToSetPlayerIsMovingToFalse = 1;
    [HideInInspector]
    [Tooltip("Add here player actionPoints")] public float PlayerPoints;
    [HideInInspector]
    public float playerStartPoints;
    [HideInInspector]
    public float pointsForAttack;

    [HideInInspector]
    public Transform movepoint;
    [HideInInspector]
    public LayerMask stopsMovement, enemyMask, playerMask;
    [HideInInspector]
    public bool isActive = false, firstClickDone = false, indicatorCanMove = false, playerIsAttacking = false, playerIsMoving = false, movingIndicatorIsOn = false, playerIsDead = false;
    [HideInInspector]
    private float wantedHP, playerHp;
    [HideInInspector]
    [SerializeField] int enemySingleShotDamage;


    [HideInInspector]
    [SerializeField] TMP_Text playerPointsText, playerAttackCostText, SpecialTileEffectText;

    Unit playerUnit;

    SFXManager sFXManager;

    SingleTargetAttack singleTargetAttack;
    [HideInInspector]
    [SerializeField] GameObject ammoPrefab;
    [HideInInspector]
    [SerializeField] Transform ammoSpawnPoint, movingIndicatorSpawnPoint;
    [HideInInspector]
    public Transform bulletTargetRange;

    BattleSystem battleSystem;
    [HideInInspector]
    [SerializeField] HealthBar healthBar;

    EnemyProto enemyProto;
    [HideInInspector]
    [SerializeField] Animator playerAnimator;
    [HideInInspector]
    [Tooltip("Just drag this characters canvas in here!")] [SerializeField] Animator canvasAnimator;


    // These are for mobile swipe control system
    [Header ("SWIPE CONTROL VARIABLES")]
    Vector2 startTouchPosition;
    Vector2 currentPosition;
    Vector2 endTouchPosition;
    bool stopTouch = false;
    [HideInInspector]
    public float swipeRange;
    [HideInInspector]
    public float tapRange;

    private void Awake()
    {
        playerUnit = GetComponent<Unit>();

        playerStartPoints = playerUnit.playerActionPoints;
        PlayerPoints = playerStartPoints;

        playerPointsText.text = PlayerPoints.ToString();
        battleSystem = FindObjectOfType<BattleSystem>();
        
        
        
        wantedHP = playerUnit.maxHP;
        playerHp = wantedHP;

        

        singleTargetAttack = FindObjectOfType<SingleTargetAttack>();

        
        ammoRange = singleTargetAttack.bulletRange / 2;

        if (FindObjectOfType<EnemySingleShoot>().enabled)
        {
            enemySingleShotDamage = FindObjectOfType<EnemySingleShoot>().bulletDamage;
        }
        

        sFXManager = FindObjectOfType<SFXManager>();

        pointsForAttack = GetComponent<Unit>().AttackCost;
    }

    private void Start()
    {
        movepoint.parent = null;

        healthBar.SetMaxValue(wantedHP);

        bulletTargetRange.transform.position = bulletTargetRange.transform.position + new Vector3(0, ammoRange, 0);

        playerAttackCostText.text = pointsForAttack.ToString();

    }

    private void Update()
    {
        PlayerActions();

        Swipe();  // related to swipe controls

        ammoRange = singleTargetAttack.bulletRange / 2;

        

        playerPointsText.text = PlayerPoints.ToString();

        if (playerHp <= 0)
        {
            
            for (int i = 0; i < playeers.Count; i++)
            {

                playeers[i].GetComponent<PlayerMovementGrid>().playeers.Remove(this.gameObject);
                
                StartCoroutine(TurnPlayerDead());
                battleSystem.players.Remove(this.gameObject);
                if (playerIsDead)
                {
                    battleSystem.CountingPlayers();
                    Destroy(this.gameObject);
                    playerIsDead = false;
                }
                
            }

                

            
        }

        if (isActive == false)
        {
            selectedPlayerIcon.SetActive(false);
        }

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

    IEnumerator TurnPlayerDead()
    {
        yield return new WaitForSeconds(.5f);

        playerIsDead = true;
        if(battleSystem.playerCount <= 1)
        {
            Destroy(this.gameObject);
            battleSystem.CountingPlayers();
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
                //this for tap functionality
            }*/
        }
    }

    private void OnMouseDown()
    {
        // functions for mouseClick/tap in mobile

        if(this.isActive == false)
        {
            for (int i = 0; i < playeers.Count; i++)
            {
                this.GetComponent<PlayerMovementGrid>();
                var thisUnit = this.GetComponent<Unit>();
                battleSystem.attackOneDamage = thisUnit.startDamage;
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

        canvasAnimator.SetTrigger("show");
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

                canvasAnimator.SetTrigger("hide");
                StartCoroutine(HidePlrPanelNow());

                battleSystem.AllPlayerPointsCheck();
            }
        }
        
        
        
        
    }


    public void IsActiveToFalse()
    {
        // this turns active player/players to inactive

        for(int i = 0; i < playeers.Count; i++)
        {
            //players[i].GetComponent<PlayerMovementGrid>();
            playeers[i].GetComponent<PlayerMovementGrid>().isActive = false;
            playeers[i].GetComponent<PlayerMovementGrid>().selectedPlayerIcon.SetActive(false);
            playeers[i].GetComponent<PlayerMovementGrid>().pLRPanel.SetActive(false);
            playeers[i].GetComponent<PlayerMovementGrid>().attackRangeIndicator.SetActive(false);
        }
        
    }

    IEnumerator HidePlrPanelNow()
    {
        yield return new WaitForSeconds(2);

        this.pLRPanel.SetActive(false);
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
                
                if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(-1f * horizontzlGridMultiplier, 0f, 0f), .2f, stopsMovement))
                {
                    var hit = Physics2D.CircleCast(movepoint.position + new Vector3(-1f * horizontzlGridMultiplier, 0f, 0f), 0.1f, Vector3.zero, Mathf.Infinity, (playerMask));

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

                        playerAnimator.SetBool("wait", false);
                        movingIndicatorIsOn = false;
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

                if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(1f * horizontzlGridMultiplier, 0f, 0f), .2f, stopsMovement))
                {
                    var hit = Physics2D.CircleCast(movepoint.position + new Vector3(1f * horizontzlGridMultiplier, 0f, 0f), 0.1f, Vector3.zero, Mathf.Infinity, (playerMask));

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

                        playerAnimator.SetBool("wait", false);
                        movingIndicatorIsOn = false;
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

                if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(0f, 1f * verticalGridSizeMultiplier, 0f), .2f, stopsMovement))
                {
                    var hit = Physics2D.CircleCast(movepoint.position + new Vector3(0f, 1f * verticalGridSizeMultiplier, 0f), 0.1f, Vector3.zero, Mathf.Infinity, (playerMask));

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

                        playerAnimator.SetBool("wait", false);
                        movingIndicatorIsOn = false;
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

                if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(0f, -1f * verticalGridSizeMultiplier, 0f), .2f, stopsMovement))
                {
                    var hit = Physics2D.CircleCast(movepoint.position + new Vector3(0f, -1f * verticalGridSizeMultiplier, 0f), 0.1f, Vector3.zero, Mathf.Infinity, (playerMask));

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

                        playerAnimator.SetBool("wait", false);
                        movingIndicatorIsOn = false;
                    }
                    
                }
            }
        }
    }

    //Player movements with keyboard commands ONLY DEVELOER USE!

    private void PlayerActions()
    {   
        transform.position = Vector3.MoveTowards(transform.position, movepoint.position, moveSpeed * Time.deltaTime);

        if (isActive == true && PlayerPoints >= 1 && playerIsAttacking == false)
        {
            if (Vector3.Distance(transform.position, movepoint.position) <= .05f)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * horizontzlGridMultiplier, 0f, 0f), .2f, stopsMovement))
                    {
                        var hit = Physics2D.CircleCast(movepoint.position + new Vector3(Input.GetAxisRaw("Horizontal") * horizontzlGridMultiplier, 0f, 0f), 0.1f, Vector3.zero, Mathf.Infinity, (playerMask));

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

                            playerAnimator.SetBool("wait", false);
                            movingIndicatorIsOn = false;


                        }
                    }

                }
                if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * verticalGridSizeMultiplier, 0f), .2f, stopsMovement))
                    {
                        var hit = Physics2D.CircleCast(movepoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical") * verticalGridSizeMultiplier, 0f), 0.2f, Vector3.zero, Mathf.Infinity, (playerMask));

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

                            playerAnimator.SetBool("wait", false);
                            movingIndicatorIsOn = false;

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
        //Damage specialTile Functionality when entering

        if(collision.gameObject.tag == "AttackBoost")
        {
            if (collision.GetComponent<AttackTile>().damageNumberIsNegative == false)
            {

                battleSystem.attackBoostIsOn = true;

                battleSystem.attackOneDamage = battleSystem.attackOneDamage + collision.GetComponent<AttackTile>().damageMultiplier;

                // For showing player special tiles effect to damage.
                GameObject tileEffectPrefab = Instantiate(specialTileEffectPrefab, ammoSpawnPoint.position, Quaternion.identity);
                tileEffectPrefab.GetComponentInChildren<TextMeshPro>().text = "+" + collision.GetComponent<AttackTile>().damageMultiplier;
                tileEffectPrefab.GetComponentInChildren<SpriteRenderer>().sprite = iconSprites[0];

                // for hiding above
                Destroy(tileEffectPrefab, timeToShowSpecialTileEffect);


                sFXManager.attackTile.Play();
            }
            if(collision.GetComponent<AttackTile>().damageNumberIsNegative == true)
            {

                battleSystem.attackBoostIsOn = true;

                battleSystem.attackOneDamage = battleSystem.attackOneDamage - collision.GetComponent<AttackTile>().damageMultiplier;

                // For showing player special tiles effect to damage.
                GameObject tileEffectPrefab = Instantiate(specialTileEffectPrefab, ammoSpawnPoint.position, Quaternion.identity);
                tileEffectPrefab.GetComponentInChildren<TextMeshPro>().text = "-" + collision.GetComponent<AttackTile>().damageMultiplier;
                tileEffectPrefab.GetComponentInChildren<SpriteRenderer>().sprite = iconSprites[0];

                // for hiding above
                Destroy(tileEffectPrefab, timeToShowSpecialTileEffect);


                sFXManager.attackTile.Play();
            }
            
        }
        if(collision.gameObject.tag == "EnemyBullet")
        {
            PlayerTakeDamage(enemySingleShotDamage);
            playerAnimator.SetTrigger("TakeDamage");
        }

        // Defence specialTile functionality when entering

        if(collision.gameObject.tag == "DefenceTile")
        {
            enemySingleShotDamage = enemySingleShotDamage / FindObjectOfType<DefenceTile>().armourAmount;

            // For showing player special tiles effect to damage.
            GameObject tileEffectPrefab = Instantiate(specialTileEffectPrefab, ammoSpawnPoint.position, Quaternion.identity);
            tileEffectPrefab.GetComponentInChildren<TextMeshPro>().text = "/" + FindObjectOfType<DefenceTile>().armourAmount.ToString();
            tileEffectPrefab.GetComponentInChildren<SpriteRenderer>().sprite = iconSprites[3];

            // for hiding above
            Destroy(tileEffectPrefab, timeToShowSpecialTileEffect);

            sFXManager.attackTile.Play();
        }

        //Range specialTile functionality when entering
        if(collision.gameObject.tag == "RangeTile")
        {
            if(collision.GetComponent<RangeTile>().RangeNumberIsNegative == false)
            {
                singleTargetAttack.bulletRange = singleTargetAttack.bulletRange + collision.GetComponent<RangeTile>().rangeAmount;
                //float extraRange = FindObjectOfType<RangeTile>().rangeAmount;
                //Debug.Log(extraRange);
                bulletTargetRange.transform.position = bulletTargetRange.transform.position + new Vector3(0, collision.GetComponent<RangeTile>().rangeAmount / 2f, 0); 

                // For showing player special tiles effect to damage.
                GameObject tileEffectPrefab = Instantiate(specialTileEffectPrefab, ammoSpawnPoint.position, Quaternion.identity);
                tileEffectPrefab.GetComponentInChildren<TextMeshPro>().text = "+" + collision.GetComponent<RangeTile>().rangeAmount;
                tileEffectPrefab.GetComponentInChildren<SpriteRenderer>().sprite = iconSprites[1];

                // for hiding above
                Destroy(tileEffectPrefab, timeToShowSpecialTileEffect);

                sFXManager.attackTile.Play();
            }
            if (collision.GetComponent<RangeTile>().RangeNumberIsNegative == true)
            {
                singleTargetAttack.bulletRange = singleTargetAttack.bulletRange - collision.GetComponent<RangeTile>().rangeAmount;
                //float extraRange = FindObjectOfType<RangeTile>().rangeAmount;
                //Debug.Log(extraRange);
                bulletTargetRange.transform.position = bulletTargetRange.transform.position - new Vector3(0, collision.GetComponent<RangeTile>().rangeAmount / 2f, 0); 

                // For showing player special tiles effect to damage.
                GameObject tileEffectPrefab = Instantiate(specialTileEffectPrefab, ammoSpawnPoint.position, Quaternion.identity);
                tileEffectPrefab.GetComponentInChildren<TextMeshPro>().text = "-" + collision.GetComponent<RangeTile>().rangeAmount;
                tileEffectPrefab.GetComponentInChildren<SpriteRenderer>().sprite = iconSprites[1];

                // for hiding above
                Destroy(tileEffectPrefab, timeToShowSpecialTileEffect);

                sFXManager.attackTile.Play();
            }
            
        }

        // Actionpoint specialTile functionality when entering

        if(collision.gameObject.tag == "ActionPointTile")
        {
            if(collision.GetComponent<ActionPointTile>().actionpointIsNegative == false)
            {
                PlayerPoints = PlayerPoints + collision.GetComponent<ActionPointTile>().extraActionPoints;
                battleSystem.allPlayerPoints = battleSystem.allPlayerPoints + collision.GetComponent<ActionPointTile>().extraActionPoints;

                battleSystem.AllPlayerPointsCheck();

                // For showing player special tiles effect to damage.
                GameObject tileEffectPrefab = Instantiate(specialTileEffectPrefab, ammoSpawnPoint.position, Quaternion.identity);
                tileEffectPrefab.GetComponentInChildren<TextMeshPro>().text = "+" + collision.GetComponent<ActionPointTile>().extraActionPoints.ToString();
                tileEffectPrefab.GetComponentInChildren<SpriteRenderer>().sprite = iconSprites[2];

                // for hiding above
                Destroy(tileEffectPrefab, timeToShowSpecialTileEffect);

                sFXManager.attackTile.Play();
            }
            if(collision.GetComponent<ActionPointTile>().actionpointIsNegative == true)
            {
                if(PlayerPoints < collision.GetComponent<ActionPointTile>().extraActionPoints)
                {
                    collision.GetComponent<ActionPointTile>().extraActionPoints = PlayerPoints;

                    PlayerPoints = PlayerPoints - collision.GetComponent<ActionPointTile>().extraActionPoints;

                    battleSystem.allPlayerPoints = battleSystem.allPlayerPoints - collision.GetComponent<ActionPointTile>().extraActionPoints;
                }
                else
                {
                    PlayerPoints = PlayerPoints - collision.GetComponent<ActionPointTile>().extraActionPoints;

                    battleSystem.allPlayerPoints = battleSystem.allPlayerPoints - collision.GetComponent<ActionPointTile>().extraActionPoints;
                }

               

                // For showing player special tiles effect to damage.
                GameObject tileEffectPrefab = Instantiate(specialTileEffectPrefab, ammoSpawnPoint.position, Quaternion.identity);
                tileEffectPrefab.GetComponentInChildren<TextMeshPro>().text = "-" + collision.GetComponent<ActionPointTile>().extraActionPoints.ToString();
                tileEffectPrefab.GetComponentInChildren<SpriteRenderer>().sprite = iconSprites[2];

                // for hiding above
                Destroy(tileEffectPrefab, timeToShowSpecialTileEffect);

                sFXManager.attackTile.Play();
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Damage specialTile functionality when exiting

        if (collision.gameObject.tag == "AttackBoost")
        {
            if (collision.GetComponent<AttackTile>().damageNumberIsNegative == false)
            {

                battleSystem.attackBoostIsOn = false;

                battleSystem.attackOneDamage = battleSystem.attackOneDamage - collision.GetComponent<AttackTile>().damageMultiplier;

                // For showing player special tiles effect to damage.
                GameObject tileEffectPrefab = Instantiate(specialTileEffectPrefab, ammoSpawnPoint.position, Quaternion.identity);
                tileEffectPrefab.GetComponentInChildren<TextMeshPro>().text = "-" + collision.GetComponent<AttackTile>().damageMultiplier;
                tileEffectPrefab.GetComponentInChildren<SpriteRenderer>().sprite = iconSprites[0];

                // for hiding above
                Destroy(tileEffectPrefab, timeToShowSpecialTileEffect);


                sFXManager.attackTile.Play();
            }
            if (collision.GetComponent<AttackTile>().damageNumberIsNegative == true)
            {

                battleSystem.attackBoostIsOn = false;

                battleSystem.attackOneDamage = battleSystem.attackOneDamage + collision.GetComponent<AttackTile>().damageMultiplier;

                // For showing player special tiles effect to damage.
                GameObject tileEffectPrefab = Instantiate(specialTileEffectPrefab, ammoSpawnPoint.position, Quaternion.identity);
                tileEffectPrefab.GetComponentInChildren<TextMeshPro>().text = "+" + collision.GetComponent<AttackTile>().damageMultiplier;
                tileEffectPrefab.GetComponentInChildren<SpriteRenderer>().sprite = iconSprites[0];

                // for hiding above
                Destroy(tileEffectPrefab, timeToShowSpecialTileEffect);


                sFXManager.attackTile.Play();
            }

        }

        //Defence specialTile functionality when exiting

        if (collision.gameObject.tag == "DefenceTile")
        {
            enemySingleShotDamage = FindObjectOfType<EnemySingleShoot>().bulletDamage;
        }

        // Range specialTile functionality when exiting

        if (collision.gameObject.tag == "RangeTile")
        {
            if(collision.GetComponent<RangeTile>().RangeNumberIsNegative == false)
            {
                singleTargetAttack.bulletRange = FindObjectOfType<RangeTile>().starBulletRange;
                //float extraRange = FindObjectOfType<RangeTile>().rangeAmount;
                //Debug.Log(extraRange);
                bulletTargetRange.transform.position = bulletTargetRange.transform.position - new Vector3(0, collision.GetComponent<RangeTile>().rangeAmount / 2, 0);

                // For showing player special tiles effect to damage.
                GameObject tileEffectPrefab = Instantiate(specialTileEffectPrefab, ammoSpawnPoint.position, Quaternion.identity);
                tileEffectPrefab.GetComponentInChildren<TextMeshPro>().text = "-" + collision.GetComponent<RangeTile>().rangeAmount;
                tileEffectPrefab.GetComponentInChildren<SpriteRenderer>().sprite = iconSprites[1];

                // for hiding above
                Destroy(tileEffectPrefab, timeToShowSpecialTileEffect);
            }
            if(collision.GetComponent<RangeTile>().RangeNumberIsNegative == true)
            {
                singleTargetAttack.bulletRange = FindObjectOfType<RangeTile>().starBulletRange;
                //float extraRange = FindObjectOfType<RangeTile>().rangeAmount;
                //Debug.Log(extraRange);
                bulletTargetRange.transform.position = bulletTargetRange.transform.position + new Vector3(0, collision.GetComponent<RangeTile>().rangeAmount / 2, 0);

                // For showing player special tiles effect to damage.
                GameObject tileEffectPrefab = Instantiate(specialTileEffectPrefab, ammoSpawnPoint.position, Quaternion.identity);
                tileEffectPrefab.GetComponentInChildren<TextMeshPro>().text = "+" + collision.GetComponent<RangeTile>().rangeAmount;
                tileEffectPrefab.GetComponentInChildren<SpriteRenderer>().sprite = iconSprites[1];

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
            playerAnimator.SetBool("wait", true);

            this.GetComponent<PlayerMovementGrid>();
            this.attackRangeIndicator.SetActive(true);
            this.indicatorCanMove = true;

            this.StartCoroutine(MovingIIndicators());

            GameObject movingIndicator = Instantiate(movingRangeIndicator, movingIndicatorSpawnPoint.position, Quaternion.identity);

            if (this.firstClickDone == false)
            {
                this.firstClickDone = true;
            }
            
        }
       else if (movingIndicatorIsOn && playerIsAttacking == false)
        {
            if (this.firstClickDone)
            {
                playerAnimator.SetBool("wait", false);
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
        if (PlayerPoints >= pointsForAttack)
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

        battleSystem.AllPlayerPointsCheck();
    }
    IEnumerator SetPlayerIsMovingToFalse()
    {
        yield return new WaitForSeconds(timeToSetPlayerIsMovingToFalse);
        playerIsMoving = false;

        battleSystem.AllPlayerPointsCheck();
    }


}
