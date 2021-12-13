using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementGrid : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] GameObject pLRPanel;
    public Transform movepoint;
    public LayerMask stopsMovement;
    bool isActive = false;

    PlayerPointManager playerPointManager;

    private void Awake()
    {
        playerPointManager = FindObjectOfType<PlayerPointManager>();
    }

    private void Start()
    {
        movepoint.parent = null;
    }

    private void Update()
    {

        var playerOne = GameObject.Find("/Canvas/PlayerPanel");
        var playerTwo = GameObject.Find("/Canvas/PlayerTwoPanel");
        if (playerOne.activeSelf == true)
        {
            PlayerOneMovement();
        }
        
        if(playerTwo.activeSelf == true)
        {
            Debug.Log("PERSE SENT??N");
            PlayerTwoMovement();
        }
        
    }
    private void OnMouseDown()
    {
        playerPointManager.ResetPlayerPoints();
        pLRPanel.SetActive(true);
        
            isActive = true;
        

    }
    public void IsActiveToFalse()
    {
        isActive = false;
    }

    private void PlayerOneMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, movepoint.position, moveSpeed * Time.deltaTime);
        if (isActive == true && PlayerPointManager.playerPoints >= 1)
        {
            if (Vector3.Distance(transform.position, movepoint.position) <= .05f)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, stopsMovement))
                    {
                        movepoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                        PlayerPointManager.playerPoints -= 1;
                        PlayerPointManager.playerTwoPoints -= 1;

                        isActive = true;
                    }

                }
                if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, stopsMovement))
                    {
                        movepoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                        PlayerPointManager.playerPoints -= 1;
                        PlayerPointManager.playerTwoPoints -= 1;

                        isActive = true;
                    }

                }
            }
        }

    }

    private void PlayerTwoMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, movepoint.position, moveSpeed * Time.deltaTime);
        if (isActive == true && PlayerPointManager.playerTwoPoints >= 1)
        {
            if (Vector3.Distance(transform.position, movepoint.position) <= .05f)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, stopsMovement))
                    {
                        movepoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                        PlayerPointManager.playerPoints -= 1;
                        PlayerPointManager.playerTwoPoints -= 1;

                        isActive = true;
                    }

                }
                if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, stopsMovement))
                    {
                        movepoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                        PlayerPointManager.playerPoints -= 1;
                        PlayerPointManager.playerTwoPoints -= 1;

                        isActive = true;
                    }

                }
            }
        }
    }
}
