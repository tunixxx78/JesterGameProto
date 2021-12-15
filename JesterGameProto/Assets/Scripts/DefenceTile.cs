using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceTile : MonoBehaviour
{
    [SerializeField] GameObject shield;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player" || collider.gameObject.tag == "Player2")
        {
            shield.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Player2")
        {
            shield.SetActive(false);
        }
    }
}
