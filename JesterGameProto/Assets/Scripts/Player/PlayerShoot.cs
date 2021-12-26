using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void Awake()
    {
        var plr = player.GetComponent<PlayerMovementGrid>();
    }

    public void StartShooting()
    {
        var plr = player.GetComponent<PlayerMovementGrid>();
        plr.PlayerOneAttack();
    }

    public void AlternativeShooting()
    {
        var plr = player.GetComponent<PlayerMovementGrid>();
        plr.PlayerTwoAttack();
    }

    public void StopShooting()
    {
        var plr = player.GetComponent<PlayerMovementGrid>();
        plr.StopAttacking();
    }

}
