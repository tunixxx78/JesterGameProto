using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDumbMovement : MonoBehaviour
{

    [SerializeField] Transform[] moveDirections;

    public void DumbEnemyMovement()
    {
        var randomDirection = Random.Range(0, moveDirections.Length);

        if (transform.position.x <= -.7)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveDirections[0].position, 1f);
        }
        if (transform.position.x >= .7)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveDirections[1].position, 1f);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, moveDirections[randomDirection].position, 1f);
        }
    }
    
}
