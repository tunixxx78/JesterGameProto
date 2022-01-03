using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_MoveCannonball : MonoBehaviour
{
    Vector3 startpos;
    Vector3 endPos;
    public float delta = 1f;

    private void Start()
    {
        startpos = this.transform.position;
        endPos = new Vector3(startpos.x, startpos.y - 30, 0);
    }
    // Update is called once per frame
    void Update()
    {
        //Vector3 targetPos = Vector3.MoveTowards(startpos, endPos, delta);
        //this.transform.position = targetPos;
        transform.Translate(Vector2.down * delta * Time.deltaTime, Space.World);

    }
}
