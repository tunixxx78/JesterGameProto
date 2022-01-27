using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTargetAttack : MonoBehaviour
{
    [Header("LEVEL DESIGNER USE!!!")]
    [Tooltip(" Insert here bullet range in tiles, including middleScreen cap- tile! ")] public float bulletRange; // Variable for bullet range in singleshot attack.

    [Tooltip("Insert wanted maximum speed of player bullet here! ")] public float maxSpeed; // variables for exponential speedUp for bullet.
    [Tooltip(" Insert wanred minimum speed of player bulklet here !")] public float minSpeed;
    [Tooltip(" Inset wanted bullet acceleration time here in seconds! ")] public float accelerationTime;

    [HideInInspector]
    [Header("PROGRAMMER USE!!!")]
    
    [SerializeField] GameObject ammoPrefab;
    [HideInInspector]
    [SerializeField] Transform ammoSpawnPoint;
    [HideInInspector]
    public float currentSpeed, time;
    

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
