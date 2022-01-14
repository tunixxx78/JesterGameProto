using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingIndicator : MonoBehaviour
{
    [SerializeField] GameObject movingIndicator;
    Transform targetPosition;
    [SerializeField] float moveSpeed = 5;


    private void Awake()
    {
        
        this.targetPosition = FindObjectOfType<PlayerMovementGrid>().bulletTargetRange;
        
        
    }

    private void Update()
    {

            transform.position = Vector3.MoveTowards(this.transform.position, targetPosition.position, moveSpeed * Time.deltaTime);

            if (FindObjectOfType<PlayerMovementGrid>().indicatorCanMove == false)
            {
                Destroy(this.gameObject);
            }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BulletDestroyer")
        {
            Destroy(this.gameObject);
        }
    }
}
