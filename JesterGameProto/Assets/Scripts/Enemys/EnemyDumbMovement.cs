using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDumbMovement : MonoBehaviour
{

    [SerializeField] Transform[] moveDirections;
    [Tooltip("If enemy movement blocked by rocks -> maskName notGo, if blocked by other enemys -> maskName enemy ! Movement needs to be blocked with something!!!")] [SerializeField] LayerMask enemyMask;

    public void DumbEnemyMovement()
    {

        var randomDirection = Random.Range(0, moveDirections.Length);
        var hit1 = Physics2D.CircleCast(moveDirections[0].position, 0.2f, Vector3.zero, Mathf.Infinity, enemyMask);
        var hit2 = Physics2D.CircleCast(moveDirections[1].position, 0.2f, Vector3.zero, Mathf.Infinity, enemyMask);

        if(!hit1.collider && !hit2.collider)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveDirections[randomDirection].position, 1f);
        }

        else if (transform.position.x <= -.7 || !hit1.collider)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveDirections[0].position, 1f);
            Debug.Log("OIKEAAN");
        }
        else if (transform.position.x >= .7 || !hit2.collider)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveDirections[1].position, 1f);
            Debug.Log("VASEMPAAN");
        }
        else if(hit1.collider && hit2.collider)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveDirections[0].position, 0f);
            Debug.Log("PAIKALLAAN");
        }
        else
        {
            
        }
    }
    
}
