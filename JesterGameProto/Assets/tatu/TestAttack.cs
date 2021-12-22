using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttack : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectileSpawnPos;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void spawnProjectile()
    {
        GameObject proj = Instantiate(projectile, projectileSpawnPos.position, Quaternion.identity);

        proj.transform.localScale = projectileSpawnPos.localScale;

    }

    public void attackAnim()
    {
        animator.SetTrigger("isShooting");
    }
}
