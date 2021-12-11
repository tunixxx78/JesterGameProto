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

    private void Start()
    {
        movepoint.parent = null;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movepoint.position, moveSpeed * Time.deltaTime);
        if(isActive == true)
        {
            if (Vector3.Distance(transform.position, movepoint.position) <= .05f)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, stopsMovement))
                    {
                        movepoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    }

                }
                if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    if (!Physics2D.OverlapCircle(movepoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, stopsMovement))
                    {
                        movepoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                    }

                }
            }
        }
        
        
    }
    private void OnMouseDown()
    {
        pLRPanel.SetActive(true);
        isActive = true;
    }
}
