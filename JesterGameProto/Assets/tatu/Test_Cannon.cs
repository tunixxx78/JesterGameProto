using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Cannon : MonoBehaviour
{
    public GameObject cannonBall;
    public Transform spawnPoint;
    public GameObject smokeFX;
    SFXManager sFXManager;

    private void Awake()
    {
        sFXManager = FindObjectOfType<SFXManager>();
    }

    public void spawnCannonBall()
    {
        Instantiate(cannonBall, spawnPoint.position, Quaternion.identity);
        Instantiate(smokeFX, spawnPoint.position, Quaternion.identity);

        sFXManager.cannon.Play();
    }
}
