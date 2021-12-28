using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTargetAttack : MonoBehaviour
{
    [SerializeField] GameObject ammoPrefab;
    [SerializeField] Transform ammoSpawnPoint;

    public void PlayerSingleTargetAttack()
    {
        GameObject proj =  Instantiate(ammoPrefab, ammoSpawnPoint.position, Quaternion.identity);
        proj.transform.localScale = ammoSpawnPoint.localScale;
    }
}
