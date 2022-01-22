using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTargetAttack : MonoBehaviour
{
    [Header("LEVEL DESIGNER USE!!!")]
    public float bulletRange; // Variable for bullet range in singleshot attack.
    public float maxSpeed, minSpeed, accelerationTime, time; // variables for exponential speedUp for bullet.

    [Header("PROGRAMMER USE!!!")]
    [SerializeField] GameObject ammoPrefab;
    [SerializeField] Transform ammoSpawnPoint;
    public float currentSpeed;
    

    private void Start()
    {
        // setting up bullet SpeedUp.
        minSpeed = currentSpeed;
        time = 0;
    }

    public void PlayerSingleTargetAttack()
    {


        GameObject proj =  Instantiate(ammoPrefab, ammoSpawnPoint.position, Quaternion.identity);
        proj.transform.localScale = ammoSpawnPoint.localScale;
    }
}
