using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovementGrid : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] GameObject pLRPanel;
    public Transform movepoint;
    public LayerMask stopsMovement;
    bool isActive = false;
    [SerializeField] int PlayerPoints, playerStartPoints, pointsForAttack;
    [SerializeField] GameObject player;

    PlayerPointManager playerPointManager;

    [SerializeField] TMP_Text playerPointsText, playerName;

    Unit playerUnit;

    [SerializeField] GameObject ammoPrefab;
    [SerializeField] Transform ammoSpawnPoint;
    private void Awake()
    {
        playerUnit = player.GetComponent<Unit>();
        playerStartPoints = playerUnit.playerActionPoints;
        playerPointsText.text = PlayerPoints.ToString();

        playerName.text = playerUnit.unitName;
    }

    private void Start()
    {
        movepoint.parent = null;
    }

    private void Update()
    {
        PlayerActions();
        playerPointsText.text = PlayerPoints.ToString();
        
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
                        movepoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                        PlayerPoints--;
                        

                        isActive = true;
                    }

                }
                if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, stopsMovement))
                    {
                        movepoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
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

   public void Attack()
    {
        if (PlayerPoints >= pointsForAttack && isActive == true)
        {
            
        }
            
    }

    
}
