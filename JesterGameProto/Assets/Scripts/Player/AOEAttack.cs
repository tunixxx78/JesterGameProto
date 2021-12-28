using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEAttack : MonoBehaviour
{
    [SerializeField] GameObject aOEAmmoPrefab, targetIcon;
    [SerializeField] Transform ammoTargetPoint;

    public void ShowTargetIcon()
    {
        targetIcon.SetActive(true);
    }

    public void HideTargetIcon()
    {
        targetIcon.SetActive(false);
    }

    public void PlayerAOEAttack()
    {
        GameObject aoeattack = Instantiate(aOEAmmoPrefab, ammoTargetPoint.position, Quaternion.identity);
        Destroy(aoeattack, 1f);
    }
}

