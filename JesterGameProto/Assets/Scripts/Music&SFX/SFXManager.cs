using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    public AudioSource playerShoot, enemyShoot, enemyBounce, playerPreShoot, hitFromBullet, playerMoving, button, attackTile, cannon;

    public void playerShooting()
    {
        playerShoot.Play();
    }
    public void EnemyShooting()
    {
        enemyShoot.Play();
    }
    public void PushTheButton()
    {
        button.Play();
    }
}
