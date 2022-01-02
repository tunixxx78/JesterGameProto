using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEventSounds : MonoBehaviour
{
    SFXManager sFXManager;

    private void Awake()
    {
        sFXManager = FindObjectOfType<SFXManager>();
    }

    public void BouncySound()
    {
        sFXManager.enemyBounce.Play();
    }
}
