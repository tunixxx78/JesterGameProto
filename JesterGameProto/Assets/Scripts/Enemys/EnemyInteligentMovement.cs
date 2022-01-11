using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteligentMovement : MonoBehaviour
{
    [SerializeField] Transform[] raycastPoints;

    private void Awake()
    {
        InteligentMovement();
    }

    public void InteligentMovement()
    {
        for (int i = 0; i < raycastPoints.Length; i++)
        {
            //RaycastHit2D hit2D = Physics2D.Raycast(raycastPoints[i].position, raycastPoints[i].up);
            Debug.DrawRay(raycastPoints[1].position, raycastPoints[1].up * 20f, Color.red);
        }
    }
        
}
