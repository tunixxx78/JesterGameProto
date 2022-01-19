using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideThingsInScene : MonoBehaviour
{
    [SerializeField] GameObject thingOne, thingTwo;
    [SerializeField] float hideDelay;

    public void HideThings()
    {
        StartCoroutine(HideNow());
    }

    IEnumerator HideNow()
    {
        yield return new WaitForSeconds(hideDelay);

        thingOne.SetActive(false);
        thingTwo.SetActive(false);
    }
}
