using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwoShoot : MonoBehaviour
{
    public GameObject EnemyTwoBullet;
    public Transform spawnPoint;
    SFXManager sFXManager;

    private void Awake()
    {
        sFXManager = FindObjectOfType<SFXManager>();
    }

    public void spawnCannonBall()
    {
        Instantiate(EnemyTwoBullet, spawnPoint.position, Quaternion.identity);

        sFXManager.enemyShoot.Play();
    }
}
