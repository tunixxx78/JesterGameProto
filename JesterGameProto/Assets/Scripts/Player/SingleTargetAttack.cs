using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTargetAttack : MonoBehaviour
{
    [SerializeField] GameObject ammoPrefab;
    [SerializeField] Transform ammoSpawnPoint;
    public float currentSpeed, maxSpeed, minSpeed, accelerationTime, time; // variables for exponential speedUp for bullet.
    public float bulletRange; // Variable for bullet range in singleshot attack.

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
