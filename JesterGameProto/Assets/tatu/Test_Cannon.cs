using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Cannon : MonoBehaviour
{
    public GameObject cannonBall;
    public Transform spawnPoint;
    public GameObject smokeFX;
    

    public void spawnCannonBall()
    {
        Instantiate(cannonBall, spawnPoint.position, Quaternion.identity);
        Instantiate(smokeFX, spawnPoint.position, Quaternion.identity);
    }
}
